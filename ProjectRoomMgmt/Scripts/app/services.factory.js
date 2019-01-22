(function ($) {
    angular
        .module('kaiptcapp')
        .factory('brudexservices', DataService);
    DataService.$inject = ['$http', '$location', '$window'];
    function DataService($http, $location,$window) {
        var baseUrl = "";      
        return {          
            btcBuyersPageCount: getData('/Btc/BuyersPageCount'),
            btcSellersPageCount: getData('/Btc/SellersPageCount'),
            getBtcBuyersByPage: getData('/Btc/BuyersPage'),
            getBtcSellersByPage: getData('/Btc/SellersPage'),
            searchBtcSellers: postData('/Btc/SearchSellers'),
            searchBtcBuyers: postData('/Btc/SearchBuyers'),
            getLocalCoinPrice: getData('/CoinData/LocalPrice'),
            getPaymentMethods: getData('/CoinData/PaymentMethods'),
            getRefExchanges: getData('/CoinData/RefExchanges'),
            getPaymentMethodFields: getData('/CoinData/PaymentMethodFields'),
            defaultExchangeRate: postData('/CoinData/DefaultExchangeRate'),
            refExchangePrice: postData('/CoinData/RefExchangePrice'),
            createOfferAdvert: postData('/CoinAction/CreateOfferAdvert'),
            submitFormWithdrawal: postData('/Wallet/Withdraw'),
            makeDepositRequest: postData('/Wallet/DepositRequest'),
            getPendindDeposit: postData('/Wallet/PendingDeposits') ,
            paymentRececiveConfirmation: postData('/Trade/ConfirmPaymentReceived'),
            disputePayment: postData('/Trade/DisputeTrade'),
            summonTradeOpponent: postData('/Trade/SummonTradeOpponent') ,
            cancelTrade: postData('/Trade/CancelTrade'),
            getDepositTimeLeft: postData('/Trade/DepositeTimeLeft') ,
            nextTradeStep: postData('/Trade/TradeStep'),
            trackPaymentNotification: postData('/Trade/FiatPaymentStatus'),
            checkIfDepositMade: postData('/Trade/CheckDepositMade'),
            checkPaymentConfirmationStatus: postData('/Trade/CheckForPaymentConfirmation')
        };  

        function postData(endpoint) {
            return function (data, callback) {
                if (!callback) {
                    callback = data;
                    data = {};
                }
                var url = baseUrl + endpoint;
                doPost(url, data, function (err, response) {
                    if (err) {
                        console.error(err);
                        return;
                    }
                    callback(response.data);
                });
            };
        }

        function getData(url) {
           
            return function () {
                var callback =arguments[0];
                var finalUrl = '';
                 if (arguments.length > 1) {
                     if (typeof arguments[0] === 'object') {
                         console.log('the first arguments >>', arguments[0]);
                        finalUrl = url + "?" + $.param(arguments[0]);
                    } else {
                         finalUrl = url + "/" + arguments[0];
                    } 
                    callback = arguments[1]; 
                }
                console.log(finalUrl);
                doGet(finalUrl, function (err, response) {
                    if (err) {
                        console.error(err);
                        return;
                    }
                    callback(response.data);
                });
            }
        }
        
        function doPost(url,data, callback) {
             return $http.post(url, data)
                .then(function (response) {
                    if (response === null) {
                       return callback(null, {status:"07",message :"Error in response"});
                    }
                    return callback(null, response);
                })
                .catch(function (error) {
                    console.log(error);
                    callback(error);
                }); 
        }

        function doGet(endpoint, callback) {
            var url = baseUrl + endpoint;
            return $http.get(url)
               .then(function (response) {
                   if (response === null) {
                       return callback(null, { status: "07", message: "Error in response" });
                   }
                  return callback(null, response);
               })
               .catch(function (error) {
                   console.log(error);
                   return callback(error);
               });
        }


    }
})(jQuery);