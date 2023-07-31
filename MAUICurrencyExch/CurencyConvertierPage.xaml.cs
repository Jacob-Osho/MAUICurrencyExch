using Android.Media.TV;
using Newtonsoft.Json;
using RestSharp;
using static Android.Icu.Text.CaseMap;
using static Android.Provider.DocumentsContract;

namespace MAUICurrencyExch;

public partial class CurencyConvertierPage : ContentPage
{
    List<Currency> currencyList;
    string FromCode;
    string ToCode;
    public CurencyConvertierPage()
    {
        InitializeComponent();
        currencyList = new List<Currency>() {
        new Currency(){Name = "US Dollar", Code = "USD"},
        new Currency { Name = "Euro", Code = "EUR" },
        new Currency { Name = "Japanese Yen", Code = "JPY" },
        new Currency { Name = "British Pound", Code = "GBP" },
        new Currency { Name = "Swiss Franc", Code = "CHF" },
        new Currency { Name = "Canadian Dollar", Code = "CAD" },
        new Currency { Name = "Australian Dollar", Code = "AUD" },
        new Currency { Name = "New Zealand Dollar", Code = "NZD" },
        new Currency { Name = "Chinese Yuan", Code = "CNY" },
        new Currency { Name = "Hong Kong Dollar", Code = "HKD" },
        new Currency { Name = "Singapore Dollar", Code = "SGD" },
        new Currency { Name = "South Korean Won", Code = "KRW" },
        new Currency { Name = "Indian Rupee", Code = "INR" },
        new Currency { Name = "Russian Ruble", Code = "RUB" },
        new Currency { Name = "Brazilian Real", Code = "BRL" },
        new Currency { Name = "Mexican Peso", Code = "MXN" },
        new Currency { Name = "South African Rand", Code = "ZAR" }};
        FromPicker.ItemsSource = currencyList;
        ToPicker.ItemsSource = currencyList;
    }

    private void ButtonConvert_Clicked(object sender, EventArgs e)
    {
        if (FromPicker.SelectedItem == null || ToPicker.SelectedItem == null || AmountEntry.Text == null)
        {
            DisplayAlert("error", "please fill nessecery inputs", "Ok");
            return;
        }
        if (FromPicker.SelectedItem == ToPicker.SelectedItem)
        {
            DisplayAlert("error", "You cant convert to the same currency", "Ok");
            return;
        }
       /* var result =*/ ConvertCurenct(FromCode, ToCode, AmountEntry.Text);
       
    }

    private async void ConvertCurenct(string from, string to, string amount)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("apikey", "EFAOyxbcyM13X1IKhuJIhExUCxH4Oszv");//(@"https://api.apilayer.com/currency_data/convert?to=to&from=from&amount=amount");
        var json = await client.GetStringAsync($"https://api.apilayer.com/currency_data/convert?to={to}&from={from}&amount={amount}");
        var response = JsonConvert.DeserializeObject<ExchangeRate>(json);
        LblResult.Text = Math.Round(Convert.ToDecimal(response.Result), 2).ToString() + ToCode;
        //var client = new RestClient($"https://api.apilayer.com/currency_data/convert?to={to}&from={from}&amount={amount}");

        //var request = new RestRequest("GET");
        //int timeoutMilliseconds = 1000; // Adjust the timeout as needed
        //request.Timeout = timeoutMilliseconds;
        //request.AddHeader("apikey", "EFAOyxbcyM13X1IKhuJIhExUCxH4Oszv");

        //var a = client.Execute(request);


    }

    private void FromPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var currency = FromPicker.SelectedItem as Currency;
        FromCode = currency.Code;

    }

    private void ToPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var currency = ToPicker.SelectedItem as Currency;
        ToCode = currency.Code;
    }
}