namespace Solita.HelsinkiBikeApp.Server.Helpers
{
    //This is example how we can easily make app more secure by validating ingoming data
    //However this class does not cover all cases by any means
    public class DataValidator
    {
        public static bool ValidateDataJourneysData(DateTime? departureDate, int pageNumber, int pageSize, out string errorMessage)
        {
            if (departureDate.HasValue && departureDate.Value.Year != 2021)
            {
                errorMessage = "Invalid departure date year";
                return false;
            }

            if (pageNumber < 1 || pageNumber > 100000)
            {
                errorMessage = "Invalid page number";
                return false;
            }

            if (pageSize < 0 || pageSize > 1000)
            {
                errorMessage = "Invalid page size";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public static bool ValidateBikeStationData(string stationName, int pageNumber, int pageSize, out string errorMessage)
        {
            if (!string.IsNullOrEmpty(stationName) && stationName.Length > 100)
            {
                errorMessage = "Invalid station name";
                return false;
            }

            if (pageNumber < 1 || pageNumber > 100000)
            {
                errorMessage = "Invalid page number";
                return false;
            }

            if (pageSize < 0 || pageSize > 1000)
            {
                errorMessage = "Invalid page size";
                return false;
            }

            errorMessage = null;
            return true;
        }

    }
}
