# JPDB-Deck-Scraper-GUI

## This project has been abandoned

If you want to filter which words you want to scrape, change the [scrape options](https://github.com/hopto-dot/JPDB-Deck-Scraper-GUI#scrape-options), change [search result filters](https://github.com/hopto-dot/JPDB-Deck-Scraper-GUI#search-result-filters) to change how and which decks are shown when you search for one.

![image](https://user-images.githubusercontent.com/66906618/114770465-64005a80-9d63-11eb-8502-25597ceb7ef9.png)


# Parameters

## Scrape Options

### Start And End
The two boxes at the top right change which word you will start and end scraping. If you have these values as 300 to 1000 and leave the other parameters as default, it will get from the 300th most common word to 1000th that appears in that deck.

![image](https://user-images.githubusercontent.com/66906618/114902736-05dc8180-9e0e-11eb-9d8b-dbdf7c975973.png)

### Reverse
If you tick the "Reverse" box, it will scrape from the last word. This means if enabled with the scrape 0 to 500, it will scrape the 500 least frequent words in that deck.

![image](https://user-images.githubusercontent.com/66906618/114758918-6c519900-9d55-11eb-869b-31dd28037a42.png)

### Output Type
You can choose to output either words or kanji.

![image](https://user-images.githubusercontent.com/66906618/114759209-c8b4b880-9d55-11eb-84d6-2527e25ad304.png)

### Scrape Filter
You can how the filter which changes the order in which words are scraped.
* DeckFreq - Orders words by how frequently they appear in the deck
* GlobalFreq - Orders words by how frequently they appear across all decks on jpdb.io
* Time - Orders words by chronological order they appear in the content.

![image](https://user-images.githubusercontent.com/66906618/114759320-ebdf6800-9d55-11eb-864e-baac497024bd.png)

## Search Result Filters

### Content Type
Narrow down the search results to a specific type of content when searching for content to scrape.

![image](https://user-images.githubusercontent.com/66906618/114759669-614b3880-9d56-11eb-9569-e6af069b24c5.png)

### Result Ordering
Change the order in which results are shown when searching for content to scrape.

![image](https://user-images.githubusercontent.com/66906618/114760640-6fe61f80-9d57-11eb-8878-cc1b6190fb60.png)
