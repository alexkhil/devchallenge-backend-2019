# Photostudios

## Publish to heroku

Build image
docker build -t photostudios-web -f src/Photostudios.Web/Dockerfile .

Publish to heroku

docker login --username= --password=$(heroku auth:token) registry.heroku.com

docker login --username=alex.khil.dev@gmail.com --password=d4f24ccf-f1c1-4f5b-9769-65f557d0b6db registry.heroku.com



docker tag photostudios-web registry.heroku.com/photostudios-web/web

docker push registry.heroku.com/photostudios-web/web

heroku container:release web -a photostudios-web



"ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=Photostudios;Trusted_Connection=True;MultipleActiveResultSets=true"