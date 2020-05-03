docker build -t SampleSite-image .

docker tag SampleSite-image registry.heroku.com/SampleSite/web


docker push registry.heroku.com/SampleSite/web

heroku container:release web -a SampleSite