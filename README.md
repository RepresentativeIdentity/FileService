#**Upute**

##1. Kloniranje repozitorija
  - otvoriti Visual Studio 
  - ukoliko nije postaviti Team Foundation Server kao izvor (Tools -> Options -> Soruce Control -> Visual Studio Team Foundation Server)
  - za povlačenje repozitorija unutar Visual Studio-a: View -> Team Explorer -> Connect (ispod Hosted Service Providers) -> koristiti vlastiti račun za spajanje na TFS -> odabrati F4bServisPrilozi
  - odabrati putanju (path) gdje će se spremiti projekt

##2. Korištenje servisa
  - otvoriti Solution FileService
  - otvoriti appsettings.json i urediti dio stringa za spajanje na bazu koji se odnosi za atribute user i password s vlastitim podacima ("ConnectionStrings" -> "DocSysBeta2" -> npr. "...;user=moj.racun;password=pristupSqlBazi")

###2.1 Za lokalno pokretanje:
  - unutar appsettings.json ("Logging" -> "Local" -> "IsDevelopment") treba postaviti na *true* i spremiti

###2.2. Za produkciju:
  - unutar appsettings.json ("Logging" -> "Local" -> "IsDevelopment") treba postaviti na *false* i spremiti


##3. Web Api-ji

  - api/attachment/document/{DProtocolID} - dohvaća prvi prilog za zadani dokument
  - api/attachment/document/all/{DProtocolID} - dohvaća sve priloge za zadani dokument 
  - api/attachment/{BinDataID} - dohvaća prilog za zadani ID priloga

###Primjeri:
  - api/attachment/document/1002
  - api/attachment/document/all/1002
  - api/attachment/331663