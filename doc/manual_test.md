# RDFSharp Manuális tesztelése felhasználói szemszögből:

Az RDFSharp library egy átlagos felhasználó (itt átlagos programozó) szemszögéből, 
ahol az alapvető feltételezés, hogy amire a dokumentáció nem tér ki részletesen, 
ott a leglogikusabb kimenetelre számítunk.

## 1.	teszt: RDF resource létrehozása.
1.1.	Resource létrehozás helyes uri string-gel:  
Elvárt kimenet: Sikeresen létrehozott RDF resource a megadott uri-val.  
Kimenet: Sikeresen létrehozott RDF resource a megadott uri-val.  

1.2.	Resource létrehozás üres string-gel:  
Elvárt kimenet: Blank resource létrehozás hasonlóan a paraméter nélküli létrehozáshoz.  
Kimenet: Exception.  

1.3.	Resource létrehozás null étrékkel:  
Elvárt kimenet: Blank resource létrehozás hasonlóan a paraméter nélküli létrehozáshoz.  
Kimenet: Exception.  


1.4.	Resource létrehozás paraméter nélkül:  
Elvárt kimenet: Blank resource létrehozás.  
Kimenet: Blank resource létrehozás.  

1.5.	Resource létrehozás nem uri formájú string-gel:  
Elvárt kimenet: Exception.  
Kimenet: Exception.  

## 2.	teszt: RDF Plainliteral létrehozása:
2.1.	RDFPlainLiteral létrehozás egyszerű string-gel:  
Elvárt kimenet: Sikeresen létrehozott RDFPlainLiteral a megadott sring-gel.  
Kimenet: Sikeresen létrehozott RDFPlainLiteral a megadott sring-gel.  

2.2.	RDFPlainLiteral létrehozás üres string-gel:  
Elvárt kimenet: Sikeresen létrehozott RDFPlainLiteral üres sring-gel.  
Kimenet: Sikeresen létrehozott RDFPlainLiteral üres sring-gel.  

2.3.	RDFPlainLiteral létrehozás null étrékkel:  
Elvárt kimenet: Sikeresen létrehozott RDFPlainLiteral üres sring-gel.  
Kimenet: Sikeresen létrehozott RDFPlainLiteral üres sring-gel.  

2.4.	RDFPlainLiteral létrehozás egyszerű srting-gel és languag tag-gel:  
Elvárt kimenet: Sikeresen létrehozott RDFPlainLiteral a megadott sring-gel és language tag-gel.  
Kimenet: Sikeresen létrehozott RDFPlainLiteral a megadott sring-gel és language tag-gel.  

2.5.	RDFPlainLiteral létrehozás egyszerű srting-gel és üres string languag tag-gel:  
Elvárt kimenet: Sikeres létrehozás üres language tag-gel.  
Kimenet: Sikeres létrehozás üres language tag-gel.  

2.6.	RDFPlainLiteral létrehozás egyszerű srting-gel és null languag tag-gel:  
Elvárt kimenet: Sikeres létrehozás üres language tag-gel.  
Kimenet: Sikeres létrehozás üres language tag-gel.  

2.7.	RDFPlainLiteral létrehozás null értékkel és null language tag-gel:  
Elvárt kimenet: Sikeres létrehozás üres értékkel és üres language tag-gel.  
Kimenet: Sikeres létrehozás üres értékkel és üres language tag-gel.  

## 3.	teszt: RDFTypedLiteral létrehozása:
3.1.	RDFTypedLiteral létrehozás megfelelő értékkel:  
Elvárt kimenet: Sikeres létrehozás.  
Kimenet: Sikeres létrehozás.  

3.2.	RDFTypedLiteral létrehozás null értékkel:  
Elvárt kimenet: Exception.  
Kimenet: Exception  

3.3.	RDFTypedLiteral létrehozás üres értékkel:  
Elvárt kimenet: Exception.  
Kimenet: Exception.  

3.4.	RDFTypedLiteral létrehozás nem megfelelő értékkel:  
Elvárt kimenet: Exception.  
Kimenet: Exception  

## 4.	teszt: RDFTriple létrehozása:
4.1.	RDFTriple létrehozás létező elemekkel:  
Elvárt kimenet: Sikeres létrehozás.  
Kimenet: Sikeres létrehozás.  

4.2.	RDFTriple létrehozás blank Resource elemekkel:  
Elvárt kimenet: Exception.  
Kimenet: Exception  

## 5.	teszt: RDFGraph létrehozása:
5.1.	RDFGraph létrehozás üres konstruktorral:  
Elvárt kimenet: Sikeres létrehozás default paraméterekkel.  
Kimenet: Sikeres létrehozás default paraméterekkel.  

5.2.	RDFGraph létrehozás RDFTriple listából:  
Elvárt kimenet: Létrehozza a gráfot a megadott RDFTriple elemekkel.  
Kimenet: Létrehozza a gráfot a megadott RDFTriple elemekkel.  
 

5.3.	RDFGraph létrehozás context-ből:  
Elvárt kimenet: Létrehozza a gráfot a megadott context-tel.  
Kimenet: Nincs ilyen constructor.  

5.4.	RDFGraph létrehozás uri-ból:  
Elvárt kimenet: Létrehozza a gráfot az uri-ról kapott adatbázisról.  
Kimenet: Exeption.  

## 6.	teszt: DataTable létrehozása RDFGraph-ból:
6.1.	DataTable létrehozás RDFGraph-ból, ami tartalmaz Triple elemeket:  
Elvárt kimenet: Sikeresen létrehozott Triple-ökből összerakott tábla.  
Kimenet: Sikeresen létrehozott Triple-ökből összerakott tábla.  

6.2.	DataTable létrehozás RDFGraph-ból, ami nem tartalmaz Triple elemeket:  
Elvárt kimenet: Üres tábla létrehozás.  
Kimenet: Üres tábla létrehozás.  


## 7.	teszt: RDFGraph file kezelése:
7.1.	RDFGraph file-ba írása:  
Elvárt kimenet: File létrehozás az RDFTriple elemekkel.  
Kimenet: File létrehozás az RDFTriple elemekkel.  

7.2.	RDFGraph file-ból betöltése:  
Elvárt kimenet: File-ból létrehozza a RDFGraph-ot az RDFTriple elemekkel.  
Kimenet: File-ból létrehozza a RDFGraph-ot az RDFTriple elemekkel.  





