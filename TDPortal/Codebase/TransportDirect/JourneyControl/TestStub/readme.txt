
The CJP test stub looks in the "Properties.xml" file in the Debug directory
to load the properties.

The Properties MUST contain the following information.

1. MinDelay and MaxDelay - these values specify the minimum and maximum network
delay to be simulated by the JourneyPlan() method.  For example, if MinDelay
is set to 1 and MaxDelay is set to 5, then the JourneyPlan() method will be
delayed by 1 to 5 seconds before it returns.  The reason for this is simply
to mimic the network delay that may occur when accessing the real CJP.
If both values are set to 0, then no delay will occur.

2. NumberOfJourneyResults - This indicates the number of journey results that
must be read by the CJP.  This number MUST match the number of Xml files provided
(see below).

3. JourneyResultPath - This specifies the name of the Xml file that contains a
JourneyResult.  Each file must CONTAIN ONLY ONE JourneyResult.
The constructor in the CJP will go through each Xml file specified
and deserialise it to recreate a JourneyResult object.

The JourneyPlan() method in the CJP test stub will return the JourneyResults
deserialised from the Xml file in sequence.  For example, if 5 Xml files were provided,
then 5 journey results will be recreated.  The first call of JourneyPlan will return
the first JourneyResult that was deserialised.  The second call will return the
second JourneyResult and so on.  When the fifth JourneyResult is returned, the next
call of JourneyResult will return the first JourneyResult again (i.e. a loopback
occurs once the end of the JourneyResults has been reached).  This implies that
if only one JourneyResult Xml file is provided, then this same result will be
returned over and over again by the JourneyPlan method.

Creating the Xml Files
----------------------
The data for the xml files can be created manually (can be quite a painful process!),
or by using the DEL3 CJP Prototype provided by Atkins.  The prototype will allow
you to select some criteria for a JourneyResult and request a JourneyPlan. The
result is then outputted in Xml format (see the ResultForm of the prototype).
Simply copy the JourneyResult Xml data (the middle section of the ResultForm),
paste into a file, and then set the Properties Xml file of the CJP Stub to point to it -
(remember that the number of files specified and the number of filenames provided MUST
MATCH!!).  The Xml file will then be loaded by the CJP stub when it is constructed
and you will be able to get the JourneyResult object back when calling JourneyPlan.

Feel free to get in touch with me if you require more info,

Kenny Cheung