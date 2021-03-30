/**
 * 
 */
package com.fx.marketprice.sub;

import com.fx.marketprice.feed.Price;
import com.fx.marketprice.margin.MarginCalculator;
import com.fx.marketprice.pubsubplatform.TinyPubSubContentServer;

/**
 * @author snehasish
 *
 */
public class FeedSubscriber implements ISubscriber {
	private final String myTopic = "Marketprice/Feed";

	/**
	 * 
	 */
	public FeedSubscriber() {}

	/* (non-Javadoc)
	 * @see com.fx.marketprice.ISubscriber#onMessageUpdate(java.lang.String)
	 */
	@Override
	public void onMessageUpdateCallback(final String topic, final String msg) {
		if (!topic.equalsIgnoreCase(myTopic))
			return;
		Price p = new Price(msg);
		MarginCalculator.recalculateMargin(p); // or maybe we should do notify..  
		
        TinyPubSubContentServer.getInstance().sendMessage( "Marketprice/Margin", MarginCalculator.getMarginedPrice(p.ccyPair).toJson());

	}


}
