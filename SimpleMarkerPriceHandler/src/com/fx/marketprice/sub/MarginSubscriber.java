/**
 * 
 */
package com.fx.marketprice.sub;

/**
 * @author snehasish
 *
 */
public class MarginSubscriber implements ISubscriber {
	private final String myTopic = "Marketprice/Margin";


	/**
	 * 
	 */
	public MarginSubscriber() {}

	/* (non-Javadoc)
	 * @see com.fx.marketprice.ISubscriber#onMessageUpdate(java.lang.String)
	 */
	@Override
	public void onMessageUpdateCallback(final String topic, final String marginUpdate) {
		if (!topic.equalsIgnoreCase(myTopic))
			return;
		System.out.println("\n Received new marginUpdate, replacing old one for same ccyPair -> \n" + marginUpdate); // just displaying the received message in standard console for now
	}


}
