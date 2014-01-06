Allows for parsing values from an XML document using XQuery syntax.

Based on the Parent-Child example from the Rainmeter Plugin SDK here:
https://github.com/rainmeter/rainmeter-plugin-sdk


Example skin usage:
```
[measureParent]
Measure=Plugin
Plugin=RainmeterXmlParserPlugin.dll
Uri="http://www.example.com/foo.xml"
UpdateRate=1800
XPath="//bar"

[measureChild]
Measure=Plugin
Plugin=RainmeterXmlParserPlugin.dll
Parent=measureParent
XPath="//baz"
```
