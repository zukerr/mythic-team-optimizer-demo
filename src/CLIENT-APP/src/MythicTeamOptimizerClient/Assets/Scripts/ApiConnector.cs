using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http.Headers;

public class ApiConnector
{
    public async Task PostRequest(UserInputModel userInput)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = SectionController.Instance.RestApiUrl;

            var contentToSend = JsonConvert.SerializeObject(userInput);
            var buffer = System.Text.Encoding.UTF8.GetBytes(contentToSend);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var myContent = await client.PostAsync(url, byteContent);
            var strContent = await myContent.Content.ReadAsStringAsync();
            Debug.Log(strContent);
            SectionController.Instance.OnDataLoaded(strContent);
        }
    }
}
