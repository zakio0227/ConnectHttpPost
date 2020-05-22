using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace WebAPITest2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //APIgatewayのPOSTエンドポイント
            string baseUrl = "https://$API.execute-api.$REGION.amazonaws.com/$STAGE/hogehoge";

            ConnectWebAPI connectWebAPI = new ConnectWebAPI();
            string httpResponseMessage = await connectWebAPI.CallWebApi(baseUrl);

            //AWSからのレスポンスを表示
            Console.WriteLine(httpResponseMessage);
        }
    public class ConnectWebAPI
    {
            string responseString;

            public async Task<string> CallWebApi(string url)
            {
                //WebAPIを呼び出す
                HttpResponseMessage response;
                try
                {
                    var httpClient = new HttpClient();

                    //MySQLにinsertするデータ
                    string id = "1";
                    string name = "Bob";

                    //Dictionary型に↑のデータを変換
                    var dictionary = new Dictionary<string, string>
                    {
                        { "id", $"{id}" },
                        { "name", $"{name}" },
                    };

                    //JSON形式に↑のデータを変換
                    var json = JsonConvert.SerializeObject(dictionary);

                    //application/jsonとJSONをまとめる
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //APIgatewayへエンドポイント＋JSONをPOST
                    response = await httpClient.PostAsync(url, content);

                    //帰ってきたレスポンスをstring型に変換
                    responseString = await response.Content.ReadAsStringAsync();

                }
                catch (Exception ex) //WebAPI呼び出しに失敗した場合
                {
                    response = null;
                }

                return responseString;

            }
        }

    }
}
