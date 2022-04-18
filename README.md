This document describes how to setup a test environment locally.

Needed runtimes
	1. Docker engine to run mssql and rabbitmq message bus
	2. To run client app - nodejs with npm

To bring up the containers mssql and rabbitmq this script needs to run from windows power shell in the root folder of the solution

docker-compose -f docker-compose-test-environment.yml up

Two projects ContactManger.Api and ContactManager.DataProvider need to be started simultaneously.

To run client application these two commands must be run from the folder contact-manager-client

npm install - this will compile client code in node-modules
npm start 