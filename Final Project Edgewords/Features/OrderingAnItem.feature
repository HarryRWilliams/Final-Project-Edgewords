Feature: OrderingAnItem

This feature checks that an item can be ordered and that the order number is retained

@OrderItem
Scenario: Creating An order with a Retainable Number
	Given I have Logged into my Account
	When I Add an Item to my Cart
	Then I am given an order number which matches between the order and account page
