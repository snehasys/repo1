/**
 * 
 */
package com.fx.marketprice.feed;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.stream.Stream;

import com.fx.marketprice.sub.FeedSubscriber;

/**
 * @author snehasish
 *
 */
public final class MockFxPriceFeed {
	private static final String csvPath = "./mockPrices"; // TODO read it from a static config/xml file
	private String topic;

	/**  ctor	 */
	public MockFxPriceFeed(final String topicName) {
		topic = topicName;
		try (Stream<Path> paths = Files.list(Paths.get(csvPath))){
			paths.filter(Files::isRegularFile)
				 .forEach(this::action);
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	private void action(final Path path) {			
		try (Stream<String> lines = Files.lines(path)){
			lines.forEach(msg -> {
				new FeedSubscriber().onMessageUpdateCallback(topic, msg); // I'm sure there's better way to do this, but just for simplicity

			});
		} catch (IOException e) {
			e.printStackTrace();
		}
				
	}


	/**
	 * naive Unittest
	 */
	public static void main(String[] args) {
		new MockFxPriceFeed("Marketprice/Feed");
	}

}
