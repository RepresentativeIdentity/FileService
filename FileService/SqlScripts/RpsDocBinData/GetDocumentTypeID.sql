SELECT	
DC.DocumentID,
DC.Document,
DC.DocTypeDesc,
DD.DProtocolID	

FROM RpsDocBinData DD	
INNER JOIN RpsBinData BD	
ON	BD.BinDataID = DD.BinDataID	
INNER JOIN DProtocol DP	
ON	DP.DProtocolID = DD.DProtocolID	
INNER JOIN DSDocument DC ON	DP.DocumentTypeID =	DC.DocumentID

WHERE DD.BinDataID = @BinDataID
ORDER BY DP.DProtocolID
