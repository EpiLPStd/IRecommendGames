import pandas as pd  # CSV filessns.set_palette('husl')
import attributeGathering
import re
import sys
import os

pd.options.mode.chained_assignment = None

try:
    path = os.path.realpath(__file__).replace("\\Skrypty\\databaseFix.py","")
    games = pd.read_csv(path + '\\steam.csv')
except:
    print("Nie znaleziono bazy danych 'steam.csv'. \nPobierz ja i sprobuj ponownie")
    sys.exit()

dbCopy = games.copy()

print("Wykrywanie wieloczlonowych nazw developerow...")
for i in range(len(dbCopy)):
    if i % 300 == 0:
        print(f"Sprawdzono {int(i/len(dbCopy)*100)}% bazy")
    games['developer'][i] = re.split('[,;/]', dbCopy['developer'][i], 1)[0]

print("================================================================")

print("Filtrowanie bazy danych...")
checked = 0
deleted = 0
dbCopy = games.copy()

for i in range(len(dbCopy)):
    if i % 300 == 0:
        print(f"Sprawdzono {int(i/len(dbCopy)*100)}% bazy")
    if dbCopy.iloc[i]['owners'] == "0-20000" or \
            dbCopy.iloc[i]['positive_ratings'] + dbCopy.iloc[i]['negative_ratings'] < 700 or \
            dbCopy.iloc[i]['average_playtime'] < 20 or \
            len(dbCopy.iloc[i]['name']) > 40:
        games = games.drop(i)
        deleted += 1
    checked += 1
print(f"Deleted {deleted} entries!")

games.to_csv(index=False, path_or_buf=path + '\\gamesFiltered.csv')

attributeGathering.gatherAttributes()
