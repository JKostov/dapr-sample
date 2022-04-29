# Dapr Simple Example App

## Local development

- Install the [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)

- Initialize Dapr env:

```cli
dapr init
```

- [Configure components for local development](https://docs.dapr.io/getting-started/tutorials/configure-state-pubsub/)

- [Develop app and run it using the Dapr CLI](https://docs.dapr.io/getting-started/get-started-api/)

## Deployment on Kubernetes

- Install Dapr on the cluster:

    - Using [Dapr CLI](https://docs.dapr.io/operations/hosting/kubernetes/kubernetes-deploy/#install-with-dapr-cli)
    - Using [Helm](https://docs.dapr.io/operations/hosting/kubernetes/kubernetes-deploy/#install-with-helm-advanced)

- Define Kubernetes files for your application and the required components - [examples](https://github.com/JKostov/dapr-sample)

- Apply the Kubernets files with:

```
kubectl apply
```
