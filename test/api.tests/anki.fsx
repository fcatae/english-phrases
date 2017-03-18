#r "packages/fsharp.data/2.3.2/lib/net40/FSharp.Data.dll"
#r "packages/newtonsoft.json/9.0.1/lib/net40/Newtonsoft.Json.dll"

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open Newtonsoft.Json


type UserInfo = { 
    user: string;
    isFirstLogin: bool
}

let a = {user = "fab"; isFirstLogin = true }

JsonConvert.SerializeObject "a.user"

//let body = HttpRequestBody 
Http.RequestString( 
    httpMethod = "POST",
    url = "http://localhost:5000/api/phrases",
    query = [],   
    headers = [ ContentType HttpContentTypes.Json ],
    body = TextRequest("hey")
    )

let baseAddress = "http://localhost:5000"

type PostData =
    | STR of string
    | JSON of obj

let POST url data = 
    let jsonData = JsonConvert.SerializeObject data

    Http.RequestString( 
        httpMethod = "POST",
        url = baseAddress + url,  
        body = TextRequest(jsonData),
        headers = [ ContentType HttpContentTypes.Json ]
        )



POST "/api/anki/start" """

    {"user": "yeah", "isFirstLogin": true}

"""

let JsonInput = JsonConvert.SerializeObject

let Returns input output = true





POST "/api/anki/start" 
    {user = "fab"; isFirstLogin = true }

|> Returns 1

