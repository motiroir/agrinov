using AgriNov.Models;
using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
	public class ServiceBoxSubscription : IServiceBoxSubscription
	{
		private BDDContext _DBContext;

		public ServiceBoxSubscription()
		{
			_DBContext = new BDDContext();
		}
		public void CreateDeleteDatabase()
		{
			_DBContext.Database.EnsureDeleted();
			_DBContext.Database.EnsureCreated();
		}

		// Function to generate the number of BoxContractSubscription (BCS) linked to the BoxContract
		//public void GenerateBCS(BoxContract boxContract)
		//{
		//	for (int i = 0; i < boxContract.NumberOfBCS; i++)
		//	{

		//		BoxSubscription bcs = new BoxSubscription()
		//		{
		//			StartDate = boxContract.StartingDate,
		//			EndDate = boxContract.EndingDate,
		//			SubscriptionStatus = SubscriptionStatus.IN_PROGRESS,
		//			Season = Season,
		//			UserAccount = UserAccount
		//		};


		//		boxContract.BoxContractSubscriptions.Add(bcs);

		//		GenerateBoxes(bcs, boxContract.DeliveryFrequency);

		//	}
		//}

        // Function to generate the number of boxes for one subscription depending on the frequency
  //      public void GenerateBoxes(BoxSubscription subscription, DeliveryFrequency frequency)
		//{
  //          // while currentDate <= subscription.EndingDate, we add a new box to subscription
  //          // current date start to subscription.StartingDate and we increment +7, +14 days or 1 month depending of frequency
  //          DateTime currentDate = subscription.StartingDate;

		//	while (currentDate <= subscription.EndingDate)
		//	{
  //              // we add a new box
  //              subscription.Boxes.Add(new Box{
  //                  DatePickUp = currentDate,
		//			Status = BoxStatus.NOT_COLLECTED,
		//			BoxContractSubscription = subscription,
  //              });

		//		// we set currentDate to next frequency
		//		switch(frequency){
  //                  case DeliveryFrequency.WEEKLY:
  //                      currentDate = currentDate.AddDays(7);
  //                      break;
  //                  case DeliveryFrequency.BIWEEKLY:
  //                      currentDate = currentDate.AddDays(14);
  //                      break;
  //                  case DeliveryFrequency.MONTHLY:
  //                      currentDate = currentDate.AddMonths(1);
  //                      break;
  //                  default:
  //                      break;
  //              }

  //          }
  //      }



  //      public void DeleteBoxContract(int boxContractID)
		//{
  //          BoxContract boxContract = _DBContext.BoxContracts.Find(boxContractID);

  //          // We first delete associated Boxes and ContractSubscriptions
  //          foreach (BoxSubscription subscription in boxContract.BoxContractSubscriptions.ToList())
  //          {
  //              foreach (Box box in subscription.Boxes.ToList())
  //              {
  //                  subscription.Boxes.Remove(box);
  //              }

  //              boxContract.BoxContractSubscriptions.Remove(subscription);
  //          }

  //          if (boxContract != null)
		//	{
		//		_DBContext.BoxContracts.Remove(boxContract);
		//		Save();
		//	}
			
		//}

		//public BoxContract GetBoxContractByID(int boxContractID)
		//{
		//	return this._DBContext.BoxContracts.Find(boxContractID);
		//}

		//public BoxContract GetBoxContractByID(string boxContractIDStr)
		//{
		//	int id;
		//	if (int.TryParse(boxContractIDStr, out id))
		//	{
		//		return this.GetBoxContractByID(id);
		//	}
		//	return null;
		//}

		//public List<BoxContract> GetBoxContracts()
		//{
		//	return _DBContext.BoxContracts.ToList();
		//}

		//public void InitializeTable()
  //      { 
  //      BoxContract boxC1 = new BoxContract() {
		//		Name = "Contrat LHS",
		//		ContentDescription = "Ce contrat comprend la livraison hebdomadaire de paniers contenant 3kg de légumes de saison." ,
		//		Price = 10M,
		//		DeliveryFrequency = DeliveryFrequency.WEEKLY,
		//		StartingDate = new DateTime(2024, 12, 26),
		//		EndingDate = new DateTime(2025, 04, 27),
		//		SaleStartingDate = new DateTime(2024, 11, 25),
		//		SaleEndingDate = new DateTime(2024, 12, 25),
		//		NumberOfBCS = 10
  //      };
		//	InsertBoxContract(boxC1);
			
		//	BoxContract boxC2 = new BoxContract() {
		//		Name = "Contrat LMM",
		//		ContentDescription = "Ce contrat comprend la livraison mensuelle de paniers contenant 5kg de légumes de saison.",
		//		Price = 13M,
		//		DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
  //              StartingDate = new DateTime(2024, 12, 26),
  //              EndingDate = new DateTime(2025, 04, 27),
  //              SaleStartingDate = new DateTime(2024, 11, 25),
  //              SaleEndingDate = new DateTime(2024, 12, 25),
  //              NumberOfBCS = 10
  //          };
		//	InsertBoxContract(boxC2);
			
		//	BoxContract boxC3 = new BoxContract() {
		//		Name = "Contrat LBHL",
		//		ContentDescription = "Ce contrat comprend la livraison bihebdomadaire de paniers contenant 7kg de légumes de saison.",
		//		Price = 16M,
		//		DeliveryFrequency = DeliveryFrequency.MONTHLY,
  //              StartingDate = new DateTime(2024, 12, 26),
  //              EndingDate = new DateTime(2025, 04, 27),
  //              SaleStartingDate = new DateTime(2024, 11, 25),
  //              SaleEndingDate = new DateTime(2024, 12, 25),
  //              NumberOfBCS = 10
  //          };

		//	InsertBoxContract(boxC3);

		//	boxC1.Name = "Contrat LVHS";
		//	boxC1.ContentDescription = "Ce contrat comprend la livraison hebdomadaire de paniers contenant 3kg de légumes de saison et 1kg de viande.";
		//	boxC1.Price = 13M;

		//	UpdateBoxContract(boxC1);

		//	DeleteBoxContract(boxC3.Id);
		//}

		//public void InsertBoxContract(BoxContract boxContract)
		//{
		//	//GenerateBCS(boxContract);
		//	_DBContext.BoxContracts.Add(boxContract);
		//	Save();
		//}

		//public void UpdateBoxContract(BoxContract boxContract)
		//{
		//	BoxContract oldBoxContract = this.GetBoxContractByID(boxContract.Id);
		//	if (oldBoxContract != null)
		//	{
		//		_DBContext.Entry(oldBoxContract).CurrentValues.SetValues(boxContract);
		//		Save();
		//	}
		//	Save();
		//}

		public void Save()
		{
			_DBContext.SaveChanges();
		}

		public void Dispose()
		{
			_DBContext.Dispose();
		}

	}
}
