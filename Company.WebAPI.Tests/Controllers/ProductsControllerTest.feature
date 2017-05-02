Feature: ProductsControllerTest
	In order to 商品控制器 
	As 依版本號差異取得或操控不同商品
	取得商品清單

@mytag
Scenario: 取得商品清單 V1
	Given 設定商品控制器的版本號為 1.0
	When 取得V1版本的商品清單 
	Then 驗證商品清單總共有 3 品

Scenario: 取得商品清單 V2
	Given 設定商品控制器的版本號為 2.0
	When 取得V2版本的商品清單 
	Then 驗證商品清單總共有 2 品