
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
     .
     .
     .
      The original Price received from DMA : 
     {Id:1110,  CcyPair:EUR/JPY,    Bid:119.61, Ask:119.91, TimeStamp:2020-09-21T12:01:02.110} 
    
     The original Price received from DMA : 
     {Id:1111,  CcyPair:GBP/USD,    Bid:1.25,   Ask:1.256,  TimeStamp:2020-09-21T12:01:02.112} 
    
    *******************************************
        CURRENT SNAPSHOT (post margin)
    *******************************************
     {Id:1000001106,    CcyPair:EUR/USD,    Bid:1.09989,    Ask:1.2001199999999999, TimeStamp:2020-09-21T12:01:01.001} 
     {Id:1000001111,    CcyPair:GBP/USD,    Bid:1.249875,   Ask:1.2561256,  TimeStamp:2020-09-21T12:01:02.112} 
     {Id:1000001110,    CcyPair:EUR/JPY,    Bid:119.598039, Ask:119.92199099999999, TimeStamp:2020-09-21T12:01:02.110} 
     
    ______________________________________
    FULL CONSOLE OUTPUT
    --------------------------------------
    Please see this file ./nohupOutput.txt
    Thank you..
