<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/opencti-logo.png alt="logo">


OpenCTI Installation Notes

</h1>

OpenCTI is a resource-intensive platform due to the many components required for its operation, including Elasticsearch, Redis, MinIO, RabbitMQ, OpenCTI-core, and others. The more **connectors** you use to feed threat data into OpenCTI, the more resource-demanding the platform becomes.

### Installation

*OpenCTI offers multiple deployment options, including Docker, manual installation, and even using Terraform or Kubernetes Helm Charts.*

For my setup, I chose **Docker** for deploying the OpenCTI stack. This method provides a quick and efficient deployment process, making it easy to add or manage connectors, especially with the help of **Portainer**.


### Prerequisites
1. **Docker**, you can find install instructions [here](https://docs.docker.com/engine/install/debian/) for Debian
2. (Optional) **Docker Portainer**, a very lightweight node, easy to install and has user-friendly GUI, you can find it out [here](https://docs.portainer.io/start/install-ce/server/docker/linux)

### Troubleshooting
This is the time when eveything become interesting when i encountered some kinda tricky buggies. 

First time when i was deploying the OpenCTI Stack, I encountered an issue with the 'health check' feature that prevented the stack from starting.

Error Preferences:
1. https://github.com/OpenCTI-Platform/docker/issues/322
2. https://stackoverflow.com/questions/57406409/unsupported-compose-file-version-1-0-even-when-i-have-the-right-compatability
3. https://github.com/OpenCTI-Platform/docker/pull/310


* Example of provided OpenCTI docker-compose.yml file
```bash
  opencti:
    image: opencti/platform:6.3.5
    environment:
      - NODE_OPTIONS=--max-old-space-size=8096
      - APP__PORT=8080
      - APP__BASE_URL=${OPENCTI_BASE_URL}
      - APP__ADMIN__EMAIL=${OPENCTI_ADMIN_EMAIL}
      - APP__ADMIN__PASSWORD=${OPENCTI_ADMIN_PASSWORD}
      - APP__ADMIN__TOKEN=${OPENCTI_ADMIN_TOKEN}
      - APP__APP_LOGS__LOGS_LEVEL=error
      - REDIS__HOSTNAME=redis
      - REDIS__PORT=6379
      - ELASTICSEARCH__URL=http://elasticsearch:9200
      - MINIO__ENDPOINT=minio
      - MINIO__PORT=9000
      - MINIO__USE_SSL=false
      - MINIO__ACCESS_KEY=${MINIO_ROOT_USER}
      - MINIO__SECRET_KEY=${MINIO_ROOT_PASSWORD}
      - RABBITMQ__HOSTNAME=rabbitmq
      - RABBITMQ__PORT=5672
      - RABBITMQ__PORT_MANAGEMENT=15672
      - RABBITMQ__MANAGEMENT_SSL=false
      - RABBITMQ__USERNAME=${RABBITMQ_DEFAULT_USER}
      - RABBITMQ__PASSWORD=${RABBITMQ_DEFAULT_PASS}
      - SMTP__HOSTNAME=${SMTP_HOSTNAME}
      - SMTP__PORT=25
      - PROVIDERS__LOCAL__STRATEGY=LocalStrategy
      - APP__HEALTH_ACCESS_KEY=${OPENCTI_HEALTHCHECK_ACCESS_KEY}
    ports:
      - "8080:8080"
    depends_on:
      redis:
        condition: service_healthy
      elasticsearch:
        condition: service_healthy
      minio:
        condition: service_healthy
      rabbitmq:
        condition: service_healthy
```

* Example of docker-compose.yml after troubleshooting with a bit changes
```bash
 opencti:
    image: opencti/platform:6.3.5
    environment:
      - NODE_OPTIONS=--max-old-space-size=8096
      - APP__PORT=8080
      - APP__BASE_URL=${OPENCTI_BASE_URL}
      - APP__ADMIN__EMAIL=${OPENCTI_ADMIN_EMAIL}
      - APP__ADMIN__PASSWORD=${OPENCTI_ADMIN_PASSWORD}
      - APP__ADMIN__TOKEN=${OPENCTI_ADMIN_TOKEN}
      - APP__APP_LOGS__LOGS_LEVEL=error
      - REDIS__HOSTNAME=redis
      - REDIS__PORT=6379
      - ELASTICSEARCH__URL=http://elasticsearch:9200
      - MINIO__ENDPOINT=minio
      - MINIO__PORT=9000
      - MINIO__USE_SSL=false
      - MINIO__ACCESS_KEY=${MINIO_ROOT_USER}
      - MINIO__SECRET_KEY=${MINIO_ROOT_PASSWORD}
      - RABBITMQ__HOSTNAME=rabbitmq
      - RABBITMQ__PORT=5672
      - RABBITMQ__PORT_MANAGEMENT=15672
      - RABBITMQ__MANAGEMENT_SSL=false
      - RABBITMQ__USERNAME=${RABBITMQ_DEFAULT_USER}
      - RABBITMQ__PASSWORD=${RABBITMQ_DEFAULT_PASS}
      - SMTP__HOSTNAME=${SMTP_HOSTNAME}
      - SMTP__PORT=25
      - PROVIDERS__LOCAL__STRATEGY=LocalStrategy
      - APP__HEALTH_ACCESS_KEY=${OPENCTI_HEALTHCHECK_ACCESS_KEY}
    ports:
      - "8080:8080"
    depends_on:
      - redis
      - elasticsearch
      - minio
      - rabbitmq
```
   
![OpenCTI Components Setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/opencti/opencti-images.png)



### Useful info / Important note
* Check out [opencti's documentation](https://docs.opencti.io/latest/) for more information
* Since MinIO using port 9000 for communication, you have to port-forwarding the Portainer default port to others, mine is 19000


