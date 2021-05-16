# CI és build keretrendszer készítése

Felelős:  
Szőke Tamás (SzTamas98)

## Felkészülés
Mivel nem voltam jártas a Continous integration és build keretrendszerek szervezésében, ezért a munkámat azzal kezdtem, hogy a Github által biztosított tréningjét elvégeztem (https://lab.github.com/githubtraining/github-actions:-continuous-integration). Ebben átfogó képet kaptam az actionökről, workflow-król és ezek működéséről githubon.

## Elkészítés

Ezután elkészítettem a szükséges workflow-t. Ez minden fontosabb branchen (main, 1-non-functional, 2-build-ci, 3-unit-tests, 4-manual-tests) push és pull request hatására építi a projektet és, ha abban vannak tesztek definiálva, akkor le is futtatja őket.

## A worlkflow javítása

Balázs (BACHlazs) visszajelzésére (https://github.com/BME-MIT-IET/iet-hf2021-create-a-new-team/pull/6#issuecomment-839659777), miszerint a Testek futtatása meghiúsul meg kellett javítanom a workflow-t. Itt a hiba az volt, hogy a workflow és a projekt nem azonos dotnet verziókban futott. Ezért ezt kijavítottam a workflow-ban, ami végül megoldotta a problémát.

## Tanulságok

Habár ez a projekt CI szempontból igen kicsinek tekinthető, itt is látható volt, hogy mennyi mindenre kell odafigyelni egy ilyen munka esetén. A fellépő hiba és annak megoldása jól mutatta, hogy milyen fontos a jó kommunikáció a külön branchek közt.