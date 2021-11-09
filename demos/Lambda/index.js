'use strict';

exports.handler = async (event) => {
    let key1 = '';
    let key2 = '';
    let key3 = '';
    let responseCode = 200;

    console.log("request: " + JSON.stringify(event));
    
    if (event.queryStringParameters) {
        if (event.queryStringParameters.key1) {
            console.log("Received key1 as query string parameter: " + event.queryStringParameters.key1);
            key1 = event.queryStringParameters.key1;
        }

        if (event.queryStringParameters.key2) {
            console.log("Received key2 as query string parameter: " + event.queryStringParameters.key2);
            key2 = event.queryStringParameters.key2;
        }
        
        if (event.queryStringParameters.key3) {
            console.log("Received key3 as query string parameter: " + event.queryStringParameters.key3);
            key3 = event.queryStringParameters.key3;
        }
    }

    if (event.body) {
        let body = JSON.parse(event.body)
        key1 = body.key1 || key1;
        key2 = body.key2 || key2;
        key3 = body.key3 || key3;
    }
 
    let responseMessage = `Given data was ${key1}, ${key2}, ${key3}`;

    let responseBody = {
        message: responseMessage
    };
    
    // The output from a Lambda proxy integration must be 
    // in the following JSON object. The 'headers' property 
    // is for custom response headers in addition to standard 
    // ones. The 'body' property  must be a JSON string. For 
    // base64-encoded payload, you must also set the 'isBase64Encoded'
    // property to 'true'.
    let response = {
        statusCode: responseCode,
        headers: {
            "x-custom-header" : "my custom header value"
        },
        body: JSON.stringify(responseBody)
    };
    
    console.log("response: " + JSON.stringify(response))
    
    return response;
};