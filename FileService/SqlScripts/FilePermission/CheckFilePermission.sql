SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS HasPermission 
FROM SPLI.DocumentAction DA INNER JOIN SPLI.DocumentActionACL AC 
ON DA.DocumentActionID = AC.DocumentActionID INNER JOIN dbo.DSSecurity SS 
ON AC.SID = SS.SID LEFT OUTER JOIN dbo.DSRole RR 
ON SS.SID = RR.SIDRole LEFT OUTER JOIN dbo.DSUserRole UR 
ON RR.SIDRole = UR.SIDRole 
WHERE DA.DocumentTypeID = @DocumentTypeID AND DA.ActionCode = 'SYS_DOCUMENT_ATTACHMENT' AND (UR.SIDUser = @userSID OR SS.SID = @userSID)