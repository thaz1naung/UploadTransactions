# UploadTransactions

Import file format : csv and xml

#csv  
Example:
“Invoice0000001”,”1,000.00”, “USD”, “20/02/2019 12:33:16”, “Approved”


#xml
Example:

<Transactions>
<Transaction id=”Inv00001”>
<TransactionDate>2019-01-23T13:45:10</TransactionDate>
<PaymentDetails>
<Amount>200.00</Amount>
<CurrencyCode>USD</CurrencyCode>
</PaymentDetails>
<Status>Done</Status>
</Transaction> 


#Create API methods
Example of output:

[{ “id”:”Inv00001”, “payment”:”200.00 USD”, “Status”: “D”}]
