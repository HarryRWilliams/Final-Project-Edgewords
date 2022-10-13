Feature: Purchasing Functionalities

This feature tests the function of entering a valid coupon to get a discount and creating an order with a retainable number

Background: 
Given I have Logged into my Account
When I Add an item to my Cart followed by going to the cart
| item   |
| Beanie |


@CheckPrice
Scenario Outline: Checking That a Single Item is Priced Correctly
	When I enter the '<coupon>' Code
	Then the Total Price Takes '<percentage>' off of the Original Price
	Examples: 
	| item   | coupon    | percentage |
	| Beanie | edgewords | 15         |

	@CheckPrice
	Scenario: Checking that the coupon code has to be correct
	When I enter the '<coupon>' Code
	Then the test should have failed
	Examples: 
	| coupon    |
	| none      |

	@OrderItem
	Scenario: Creating An order with a Retainable Number
	When I enter details into the order form
	| firstName | lastName | streetAddress                  | townName           | postcode | phoneNumber |
	| Dave      | Jones    | 11 In the Middle of our Street | Also not real town | SW1W 0NY | 12345678910 |
	Then I am given an order number which matches between the order and account page