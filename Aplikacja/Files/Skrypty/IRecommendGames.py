import json
import os
import sys

import pandas as pd  # CSV files
import random
import math
import datetime

pd.options.mode.chained_assignment = None

try:
    path = os.path.realpath(__file__).replace("\\Skrypty\\IRecommendGames.py","")
    games = pd.read_csv(path + '/gamesFiltered.csv')
except:
    print("Brak pliku 'gamesFiltered.csv'.")
    sys.exit()

class DataProcessing:
    @staticmethod
    def prepareSet(X, c1, prefs):
        for column in X.columns:
            if not prefs.keys().__contains__(column):
                continue
            elif column == "positive_ratings":
                for i in range(len(X)):
                    constrains, weight = prefs[column][0].split(';')
                    ratio = float(X[column][i])*100/(float(X[column][i]) + float(X['negative_ratings'][i]))
                    if float(constrains.split('-')[0]) < ratio < float(constrains.split('-')[1]):
                        X[column][i] = float(weight)
                    else:
                        X[column][i] = 0
                continue
            elif column == "release_date":
                for i in range(len(X)):
                    constrains, weight = prefs[column][0].split(';')
                    gameDateFactors = X[column][i].split('/')
                    fromDateFactors = constrains.split('-')[0].split('/')
                    toDateFactors = constrains.split('-')[1].split('/')

                    gameDate = datetime.datetime(int(gameDateFactors[2]), int(gameDateFactors[1]), int(gameDateFactors[0]))
                    fromDate = datetime.datetime(int(fromDateFactors[2]), int(fromDateFactors[1]), int(fromDateFactors[0]))
                    toDate = datetime.datetime(int(toDateFactors[2]), int(toDateFactors[1]), int(toDateFactors[0]))
                    if fromDate > gameDate:
                        X[column][i] = 0
                    elif toDate < gameDate:
                        X[column][i] = 0
                    else:
                        X[column][i] = float(weight)
                continue
            elif c1.__contains__(column):
                for i in range(len(X)):
                    constrains, weight = prefs[column][0].split(';')
                    if float(constrains.split('-')[0]) < float(X[column][i]) < float(constrains.split('-')[1]):
                        X[column][i] = float(weight)
                    else:
                        X[column][i] = 0
                continue
            for i in range(len(X)):
                weight = 0
                for value in prefs[column]:
                    if X[column][i].__contains__(value.split(';')[0]):
                        weight += float(value.split(';')[1])
                X[column][i] = weight
        return X

    @staticmethod
    def normalizeSet(X, c):
        for column in X.columns:
            if not c.__contains__(column):
                continue
            ma = float(X.max()[column])
            mi = float(X.min()[column])
            for i in range(len(X)):
                if ma - mi == 0:
                    X[column][i] = X[column][i] - mi
                else:
                    X[column][i] = (X[column][i] - mi) / (ma - mi)
        return X

    @staticmethod
    def splitSet(X):
        division = math.ceil(7 * len(X) / 10)
        train = X.iloc[:division]
        val = X.iloc[division:]
        return train, val

    @staticmethod
    def shuffle(X):
        for i in range(len(X) - 1, 0, -1):
            tmp = random.randint(0, i)
            X.iloc[i], X.iloc[tmp] = X.iloc[tmp], X.iloc[i]
        return X


class SoftSet:
    @staticmethod
    def classify(X, c, amount):
        dist = []
        for j in range(len(X)):
            l = 0
            for column in c:
                l += X.iloc[j][column]
            dist.append(l)

        tmp = dist.copy()
        tmp.sort(reverse=True)
        ma = tmp[:amount]
        r = []
        for d in ma:
            for j in range(len(dist)):
                if d == dist[j] == 0:
                    continue
                for name in r:
                    if X.iloc[j]['name'] == name:
                        continue
                if d == dist[j]:
                    r.append((X.iloc[j]['name'], dist[j]))
                if len(r) >= amount:
                    break
        return r


def getUserPrefs():
    path = os.path.realpath(__file__).replace("Skrypty\\IRecommendGames.py", "")

    try:
        with open(path + '\\UserPrefs.json') as inputFile:
            userPrefs = json.load(inputFile)
    except:
        print("Brak pliku 'UserPrefs.json'.")
        sys.exit()

    Preferences = {}
    for pref in userPrefs:
        if Preferences.keys().__contains__(pref.split(':', 1)[0]):
            Preferences[pref.split(':', 1)[0]].append(pref.split(':', 1)[1].replace('Waga:', ''))
        else:
            Preferences[pref.split(':', 1)[0]] = [pref.split(':', 1)[1].replace('Waga:', '')]
    if Preferences.keys().__contains__('Data'):
        Preferences['release_date'] = Preferences.pop('Data')
    if Preferences.keys().__contains__('Developer'):
        Preferences['developer'] = Preferences.pop('Developer')
    if Preferences.keys().__contains__('Wydawca'):
        Preferences['publisher'] = Preferences.pop('Wydawca')
    if Preferences.keys().__contains__('Platforma'):
        Preferences['platforms'] = Preferences.pop('Platforma')
    if Preferences.keys().__contains__('Kategoria'):
        Preferences['categories'] = Preferences.pop('Kategoria')
    if Preferences.keys().__contains__('Typ'):
        Preferences['genres'] = Preferences.pop('Typ')
    if Preferences.keys().__contains__('Tag'):
        Preferences['steamspy_tags'] = Preferences.pop('Tag')
    if Preferences.keys().__contains__('%_Pozytywnych_recenzji'):
        Preferences['positive_ratings'] = Preferences.pop('%_Pozytywnych_recenzji')
        Preferences['positive_ratings'] = [Preferences['positive_ratings'][0].replace('%', '')]
    if Preferences.keys().__contains__('Sredni_czas_gry'):
        Preferences['average_playtime'] = Preferences.pop('Sredni_czas_gry')
    if Preferences.keys().__contains__('Cena'):
        Preferences['price'] = Preferences.pop('Cena')
    return Preferences

userPreferences = getUserPrefs()
C1 = ['release_date', 'positive_ratings', 'price', 'average_playtime']
C2 = ['developer', 'publisher', 'platforms', 'categories', 'genres', 'steamspy_tags']
C = userPreferences.keys()
games = DataProcessing.prepareSet(games, C1, userPreferences)
gamesNormalized = DataProcessing.normalizeSet(games, C)

numberOfRecommendations = int(sys.argv[1])
recommendations = SoftSet.classify(gamesNormalized, C, numberOfRecommendations)

print("Polecane gry:")
for i in range(len(recommendations)):
    print(str(i + 1) + ") " + recommendations[i][0])
