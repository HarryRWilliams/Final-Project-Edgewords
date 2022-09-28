Feature: Checking Price Table in Cart

This feature must ensure that the coupon Edgewords adds a 15% discount to the total price

@CheckPrice
Scenario Outline: Checking That a Single Item is Priced Correctly
	Given I have Logged into my Account
	When I Add an Item to my Cart
	And I enter the '<coupon>' Code
	Then the Total Price Takes 15% off of the Original Price
	#And Logout of My Account
	Examples: 
	| coupon    |
	| edgewords |
	| none      |