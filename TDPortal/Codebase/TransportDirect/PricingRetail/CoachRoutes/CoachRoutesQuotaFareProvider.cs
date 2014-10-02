// ************************************************************** 
// NAME			: CoachRoutesQuotaFaresProvider.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Definition of CoachRoutesQuotaFaresProvider class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/CoachRoutesQuotaFareProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:38   mturner
//Initial revision.
//
//   Rev 1.1   Nov 07 2005 15:11:00   RWilby
//Updated FindRoutesAndQuotaFares method to not add a Route to the RouteList if the originNaPTAN and exchangeNaPTAN are the same.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 09:55:42   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 19 2005 18:21:28   RWilby
//Updated to comply with FxCop
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 14:46:14   RWilby
//Corrected spelling mistake in "FindRoutesAndQuotaFares"
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 11 2005 17:06:18   RWilby
//Updated Class
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:00:14   RWilby
//Initial revision.
using System;
using Logger = System.Diagnostics.Trace;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// CoachRoutesQuotaFareProvider.
	/// </summary>
	public class CoachRoutesQuotaFareProvider : ICoachRoutesQuotaFaresProvider, IServiceFactory
	{
		private CoachOperatorList coachOperatorList;

		private const string DataChangeNotificationGroup = "CoachRoutesQuotaFare";
		
		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;

		/// <summary>
		/// Constructor
		/// </summary>
		public CoachRoutesQuotaFareProvider()
		{
			LoadData();

			receivingChangeNotifications = RegisterForChangeNotification();
		}

		/// <summary>
		/// Loads the CoachOperatorList object graph
		/// </summary>
		private void LoadData()
		{
			//Synchronize operatorList
			lock(this)
			{
				//Call static method CoachOperatorList.Fetch() to return data-populated operatorList object graph
				coachOperatorList = CoachOperatorList.Fetch();
			}
		}

		/// <summary>
		/// Registers an event handler with the data change notification service
		/// </summary>
		private bool RegisterForChangeNotification()
		{
			IDataChangeNotification notificationService;
			try
			{
				notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
			}
			catch (TDException e)
			{
				// If the SDInvalidKey TDException is thrown, return false as the notification service
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing CoachRoutesQuotaFareProvider"));
					return false;
				}
				else
					throw;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChanged);
			return true;
		}

		#region Implementation of ICoachRoutesQuotaFaresProvider
		/// <summary>
		/// FindRoutesAnsQuotaFares analyses the OperatorList object graph to build up a RouteList object graph to return
		/// </summary>
		/// <param name="originNaPTAN">Origin NaPTAN location</param>
		/// <param name="destinationNaPTAN">Destination NaPTAN location</param>
		/// <returns>RouteList collection</returns>
		public RouteList FindRoutesAndQuotaFares(string originNaPTAN, string destinationNaPTAN)
		{
			RouteList routeList = new RouteList();
			
			foreach(CoachOperator op in coachOperatorList)
			{
				if(op.NaPTANDictionary.IsServed(originNaPTAN))
				{
					Leg leg = new Leg();
					leg.StartNaPTAN = originNaPTAN;
					
					//Is the NaPTAN served by the operator
					if(op.NaPTANDictionary.IsServed(destinationNaPTAN))
					{
						leg.EndNaPTAN = destinationNaPTAN;
						leg.CoachOperatorCode = op.ToString();
						leg.QuotaFareList = op.QuotaFareList.GetQuotaFares(leg.StartNaPTAN,leg.EndNaPTAN);

						Route route = new Route();
						route.OriginNaPTAN = originNaPTAN;
						route.DestinationNaPTAN = destinationNaPTAN;
						route.LegList.Add(leg);
						routeList.Add(route);
					}
					else //Check the destination naptan's ExchangeNaPTANList collection
					{
						if(op.NaPTANDictionary.Contains(destinationNaPTAN) && 
							op.NaPTANDictionary[destinationNaPTAN].ExchangeNaPTANList.Count > 0)
						{
							foreach (ExchangeNaPTAN exchangeNaPTAN in op.NaPTANDictionary[destinationNaPTAN].ExchangeNaPTANList)
							{
								foreach(CoachOperator op2 in coachOperatorList)
								{
									if(!op.Equals(op2) && 
										!op.NaPTANDictionary[destinationNaPTAN].InvalidExchangeOperatorList.IsInvalidOperator(op2.ToString()))
									{
										//If exchangeNaPTAN is served by another operator AND the first operator is not an invalid operator for the second operator at the exchanege point
										if(op2.NaPTANDictionary.IsServed(exchangeNaPTAN.ToString()) &&
											!op2.NaPTANDictionary[exchangeNaPTAN.ToString()].InvalidExchangeOperatorList.IsInvalidOperator(op.ToString()))
										{
											//If originNaPTAN and exchangeNaPTAN are the same then don't add the route
											if(string.Compare(originNaPTAN,exchangeNaPTAN.ToString(),true,CultureInfo.InvariantCulture)!=0)
											{
												//New route foreach exchange point
												Route route = new Route();
												route.OriginNaPTAN = originNaPTAN;
												route.DestinationNaPTAN = destinationNaPTAN;

												Leg legA = new Leg();
												legA.StartNaPTAN = originNaPTAN;
												legA.EndNaPTAN = exchangeNaPTAN.ToString();
												legA.CoachOperatorCode = op.ToString();
												legA.QuotaFareList = op.QuotaFareList.GetQuotaFares(legA.StartNaPTAN,legA.EndNaPTAN);
											
												//Add the first route leg
												route.LegList.Add(legA);
											
												//Get the second route leg
												Leg legB = new Leg();
												legB.StartNaPTAN = exchangeNaPTAN.ToString();
											
												if (op2.NaPTANDictionary.IsServed(destinationNaPTAN))
												{
													legB.EndNaPTAN = destinationNaPTAN;
													legB.CoachOperatorCode = op2.ToString();
													legB.QuotaFareList = op2.QuotaFareList.GetQuotaFares(legB.StartNaPTAN,legB.EndNaPTAN);

													route.LegList.Add(legB);

													routeList.Add(route);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			return routeList;
		}
		#endregion

		#region Data Changed Event handler
		/// <summary>
		/// Used by the Data Change Notification service to reload the data if it is changed in the DB
		/// </summary>
		private void DataChanged(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == DataChangeNotificationGroup)
				LoadData();
		}
		#endregion

		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current CoachRoutesQuotaFareProvider object
		/// </summary>
		/// <returns>CoachRoutesQuotaFareProvider object</returns>
		public object Get()
		{
			return this;
		}
		#endregion

	}
}
