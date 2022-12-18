# ESDP-AC-8-9-TEAM-1 - Invictus Academy

## О проекте

Данный проект призван выполнять задачу онлайн обучения тренеров.

## Запуск проекта на своём сервере

### Требования для сервера

Сервер должен быть на базе ОС Linux.  
На сервере должны быть установлены:

 - GIT:
```
sudo apt update
sudo apt install git
```

 - Docker:
```
sudo apt update
sudo apt install docker
```

 - Docker-compose:
```
sudo apt update
sudo apt install docker-compose
```

### Разворачивание проекта на сервере

```
git clone https://gitlab.com/AttractorSchool/esdp-ac-8-9-team-1.git
cp ./esdp-ac-8-9-team-1/src/.env-example ./esdp-ac-8-9-team-1/src/.env
vim ./esdp-ac-8-9-team-1/src/.env
```

Теперь вы попали в редактор текста Vim, в данном файле необходимо заменить стандартные значения переменных
на ваши, также в данном файле есть комментарии для помощи о том какие данные необходимо внести

После чего необходимо запустить docker-compose:

```
cd ./esdp-ac-8-9-team-1/src
docker-compose up -d
```

После окончания работы команды, проект запущен на сервере.

### Доступ к серверу

Ссылка на User-Frontend: http://{ваш домен}  
Ссылка на Admin-Frontend: http://{ваш домен}:8080/admin-panel  

Доступ к базам данных по логину паролю который вы настроили в файле .env  

Ссылка на PgAdmin: http://{ваш домен}:5051  
Ссылка на Mongo-Express: http://{ваш домен}:8085  

### Логирование

Логи доступны в папке ~/esdp-ac-8-9-team-1/logs  
Каждый из микросервисов пишет в отдельную папку  