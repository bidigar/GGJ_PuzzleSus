#define EOS_PRODUCT_NAME "MyFriendlySoul"
#define EOS_PRODUCT_VERSION "1.0"
#define EOS_SANDBOX_ID "81a8780b450e4b7b8b56ced5d9000962"
#define EOS_PRODUCT_ID "ca92b8d224484956abe8c285f74c5ff2"
#define EOS_DEPLOYMENT_ID "ac733cd0acd841d68a74b401ba2df3f4"
#define EOS_CLIENT_SECRET "Wixs0btcZLmyL3mirF6ceHUReFgGErqFbmToPYMsmCU"
#define EOS_CLIENT_ID "xyza7891GRDmSkkNCS5ckCBoiV1J7IgZ"
_WIN32 || _WIN64
#define PLATFORM_WINDOWS 1
#endif

#if _WIN64
#define PLATFORM_64BITS 1
#else
#define PLATFORM_32BITS 1
#endif	

extern "C" __declspec(dllexport) char*  __stdcall GetConfigAsJSONString()
{
            return "{"
              "productName:" EOS_PRODUCT_NAME ","
              "productVersion: " EOS_PRODUCT_VERSION ","
              "productID: "  EOS_PRODUCT_ID ","
              "sandboxID: "  EOS_SANDBOX_ID ","
              "deploymentID: " EOS_DEPLOYMENT_ID ","
              "clientSecret: "  EOS_CLIENT_SECRET ","
              "clientID: "  EOS_CLIENT_ID

           "}"
        ;
        }