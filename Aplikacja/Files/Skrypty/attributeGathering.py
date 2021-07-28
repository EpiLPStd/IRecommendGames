import json
import os
import sys

import pandas as pd  # CSV filessns.set_palette('husl')


def gatherAttributes():
    pd.options.mode.chained_assignment = None
    
    try:
        path = os.path.realpath(__file__).replace("\\Skrypty\\attributeGathering.py", "")
        games = pd.read_csv(path + '/gamesFiltered.csv')
    except:
        print("Brak pliku 'gamesFiltered.csv'. \nUruchom opcję 1 w menu i spróbuj ponownie.")
        sys.exit()

    cat = {'developer': [], 'publisher': [], 'platforms': [], 'categories': [], 'genres': [], 'steamspy_tags': []}

    for col in games.columns:
        if not cat.__contains__(col):
            continue
        for c in games[col]:
            for s in c.split(';'):
                if not cat[col].__contains__(s):
                    cat[col].append(s)
    
    with open(path + '/attributes.json', 'w') as outfile:
        json.dump(cat, outfile)