Feature: VersionedSpecFlowTest
	In order to 控制版本路徑
	As 透過route處理版本路徑
	開發者可使用 VersionRouteAttribute 或 VersionRangeRouteAttribute 
	設定於 API Action，限定每個版本的 Method

Scenario: 版本控管_指定可使用的版本號
	Given 指定可使用的版本號為 '2.0, 2.1'
	And 指定為預設版本控管路徑
	When 輸入的版本號為 2.0
	Then 版本路徑正常運作為 true

Scenario: 版本控管_指定不可使用的版本號
	Given 指定可使用的版本號為 '2.0, 2.1'
	And 指定為預設版本控管路徑
	When 輸入的版本號為 3.0
	Then 版本路徑正常運作為 false

Scenario: 版本控管_設定固定的區間內可使用的版本號
	Given 指定可使用的版本號為 '2.0, 2.1, 3.0'
	And 指定版本控管區間為 2.0 到 2.1
	When 輸入的版本號為 2.1
	Then 版本路徑正常運作為 true

Scenario: 版本控管_設定固定的區間內不可使用的版本號
	Given 指定可使用的版本號為 '2.0, 2.1, 3.0'
	And 指定版本控管區間為 2.0 到 2.1
	When 輸入的版本號為 3.0
	Then 版本路徑正常運作為 false

Scenario: 版本控管_設定固定的區間內最小版本號
	Given 指定可使用的版本號為 '2.0, 2.1, 3.0, 4.0'
	And 指定版本控管區間最小版本為 2.0 到最新
	When 輸入的版本號為 4.0
	Then 版本路徑正常運作為 true