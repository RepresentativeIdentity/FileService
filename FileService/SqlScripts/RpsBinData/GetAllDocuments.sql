SELECT
RpsDocBinData.DocBinDataID,
RpsDocBinData.BinDataID,
RpsDocBinData.DProtocolID,
RpsBinData.FileName,
RpsBinData.DataType,
RpsBinData.DateCreated,
RpsBinData.DataDesc,
DProtocolIDPosta=DProtocolVeza.DProtocolIDDoc
FROM
RpsDocBinData INNER JOIN RpsBinData ON
RpsDocBinData.BinDataID=RpsBinData.BinDataID
LEFT JOIN DProtocolVeza ON DProtocolVeza.DProtocolIDDoc=RpsDocBinData.DProtocolID
AND DProtocolVeza.TipVeze = 'PROTOCOLPOSTA'
WHERE
(RpsDocBinData.DProtocolID=@DProtocolID OR DProtocolVeza.DProtocolIDVeza=@DProtocolID)