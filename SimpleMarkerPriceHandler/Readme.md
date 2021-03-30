
______________________________________
    INFORMATION ON HOW TO RUN?
--------------------------------------
    The main is written here TestMainFlow (/SimpleMarkerPriceHandler/src/com/fx/marketprice/main/TestMainFlow.java)
    Please execute the main from there.

______________________________________
    DEPENDANCY
--------------------------------------
    1. Concurrent API

______________________________________
    HOW To Add New Mock DMA PRICE csv?
--------------------------------------
    add new csv's in this path /SimpleMarkerPriceHandler/mockPrices/

______________________________________
    HOW TO CHANGE THE COMMISION ALGO ?
--------------------------------------
    Feel free to change the commission algo in AlgoForMarginCalcuation 
    (i.e. com.fx.marketprice.margin.AlgoForMarginCalcuation)
    
    
______________________________________
    SAMPLE OUTPUT
--------------------------------------
     The original Price received from DMA : 
     {Id:106,   CcyPair:EUR/USD,    Bid:1.1,    Ask:1.2,    TimeStamp:2020-06-01T12:01:01.001} 
    
     Received new marginUpdate, replacing old one for same ccyPair -> 
     {Id:1000000106,    CcyPair:EUR/USD,    Bid:1.09989,    Ask:1.2001199999999999, TimeStamp:2020-06-01T12:01:01.001} 
    
     The original Price received from DMA : 
     {Id:107,   CcyPair:EUR/JPY,    Bid:119.6,  Ask:119.9,  TimeStamp:2020-06-01T12:01:02.002} 
    
     Received new marginUpdate, replacing old one for same ccyPair -> 
     {Id:1000000107,    CcyPair:EUR/JPY,    Bid:119.58803999999999, Ask:119.91199,  TimeStamp:2020-06-01T12:01:02.002} 


______________________________________
    FULL CONSOLE OUTPUT
--------------------------------------
Please see this file ./nohupOutput.txt
Thank you..
