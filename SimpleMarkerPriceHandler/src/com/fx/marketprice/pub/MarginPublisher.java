
package com.fx.marketprice.pub;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse.BodyHandler;
import java.net.http.HttpResponse.BodySubscriber;
import java.net.http.HttpResponse.ResponseInfo;
import com.fx.marketprice.feed.Price;
import com.fx.marketprice.pubsubplatform.TinyPubSubContentServer;


/**
 * @author snehasish
 *
 */
public final class MarginPublisher implements IPublisher {
	private final String topic = "Marketprice/Margin";
	private static final String fictionalURL = "https://api.mybank.org/fx/marketprice";
	private HttpClient client;
	private HttpRequest request;
	
	
	/**
	 * 
	 */
	public MarginPublisher() {
		client = HttpClient.newHttpClient();
		request = HttpRequest.newBuilder(
				  URI.create(fictionalURL)).header("accept", "json").build();
		
	}

	/* (non-Javadoc)
	 * @see com.fx.marketprice.IPublisher#publish(java.lang.String)
	 */
	@Override
	public void publish(Price price) {
		var response = client.sendAsync(request, new BodyHandler<>() {  
			@Override
			public BodySubscriber apply(ResponseInfo info) {
				// TODO convert Price into json here				
				return null; // do response.get()
			}
		});
//		System.out.println("Marginepublish publishing: " + price.toJson()); // printing to show something in my test
        TinyPubSubContentServer.getInstance().sendMessage(this.topic, price.toJson());
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

}
