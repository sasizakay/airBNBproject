using Ex1Ver6.BL;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Ex1Ver6.BL
{
    public class Vacation
    {
        string id;
        string userId;
        string flatId;
        DateTime startDate;
        DateTime endDate;
        static List<Vacation> vacationsList = new List<Vacation>();
        DBServices dbs = new DBServices();

        public Vacation()
        {

        }

        public Vacation(string id, string userId, string flatId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            UserId = userId;
            FlatId = flatId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Id { get => id; set => id = value; }
        public string UserId { get => userId; set => userId = value; }
        public string FlatId { get => flatId; set => flatId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value.Date; }
        public DateTime EndDate { get => endDate; set => endDate = value.Date; }

        /// <summary>
        /// Validates the input data, checks if the flat exists and isn't already rented.
        /// </summary>
        /// <returns>
        /// True if all condition match and adding the object to the vacations list.
        /// </returns>
        public bool Insert()
        {
            vacationsList = new Vacation().Read();
            if (!checkIfFlatExist())
            {
                return false;
            }
            if (this.startDate.Date.CompareTo(this.endDate.Date) > 0) //validate input dates
            {
                return false;
            }
            
            foreach (Vacation vacation in vacationsList) //Vacation id is unique
            {
                if (this.Id == vacation.Id)
                {
                    return false;
                }
            }
            //This loop checks if the flat is already rented
            foreach (Vacation vacation in vacationsList)
            {
                if (vacation.FlatId == this.FlatId)
                {//Search for active vacation
                    bool f1 = vacation.StartDate.Date.CompareTo(this.EndDate.Date) > 0;
                    bool f2 = vacation.EndDate.Date.CompareTo(this.StartDate.Date) < 0;
                    if (!(vacation.StartDate.Date.CompareTo(this.EndDate.Date) > 0 || vacation.EndDate.Date.CompareTo(this.StartDate.Date) < 0))
                    {//Flat is rented
                        return false;
                    }
                }
            } //Finished this loop if the flat is avilable
            
            dbs.InsertVacation(this);
            return true;

        }
        public bool checkIfFlatExist()
        {
            List<Flat> flatsList = new Flat().Read();
            foreach (Flat flat in flatsList)
            {
                if (flat.Id == this.FlatId)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Vacation> Read()
        {
            return dbs.ReadVacation();
        }
        public List<Vacation> getByDates(DateTime start, DateTime end)
        {
            return dbs.getVacationsByDates(start, end);
        }
    }
}
