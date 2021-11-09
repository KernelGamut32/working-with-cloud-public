## IaaS and PaaS - Demo

1. Create VPC using "VPC Wizard"
	- Use 10.0.0.16 for CIDR block
	- Use "launch-demo-vpc" for VPC name
	- Use 10.0.1.0/24 for public subnet CIDR block; reference link for CIDR translation/calculation
	- Talk about availability zone and choose one
	- Use "launch-demo-subnet1" for subnet name
	- Leave the rest at the defaults
	- Review after creation, including public subnet allowing routing out through IG
2. Create second public subnet
	- Use 10.0.2.0/24 for public subnet CIDR block
	- Choose a different availability zone
	- Use "launch-demo-subnet2" for subnet name
	- Talk about how availability zones provide redundancy at the region level (split across multiple data centers within the same region - high-speed connectivity between; for EC2 instances, might create multiple copies of the same instance config - for RDS, requires a subnet group across at least 2 availability zones)
    - Add route for igw to route table
3. Create new EC2 (Elastic Cloud Compute) instance - Ubuntu instance
	- Select AMI (free tier)
	- Select instance type - talk about various characteristics including purpose of EBS
	- 1 instance - talk about purpose of Auto Scaling Group
	- No public IP (for now)
	- Configure a new Security Group - uses 5-tuple format
		* Name it "launch-demo-linux-sg" | "Security Group for demo Linux VMs"
		* Talk about addition of new rule
		* Talk about wide open SSH (port 22) in production usually not a good idea - use bastion host (or jump box) instead typically (Hub & Spoke) with peering and IP restrictions
		* Update for my IP address only
	- Create new key pair for ssh and download ("launch-demo-linux-key") - manages secure access over SSH
	- While instance is building, build out Windows VM
4. Create new EC2 instance - Windows instance
	- Select AMI (free tier)
	- Select instance type - talk about various characteristics
	- 1 instance
	- Add public IP at time of creation
	- Could use existing Security Group - however, will create new for separate Windows settings (principle of "least privilege")
		* Name is "launch-demo-windows-sg" | "Security Group for demo Windows VMs"
		* Talk about wide open RDP (port 3389) in production usually not a good idea - use bastion host (or jump box) instead typically with peering and IP restrictions
	- Create new key pair and download ("launch-demo-windows-key") - used for secure access to admin password
5. While second instance is building, walk through IaaS demo diagram - route tables, ACL's and VPC/subnets
6. After creation of both instances completes
	- Assign name "launch-demo-linux-vm01" to Linux VM - makes it easier to find in dropdowns later
	- Assign name "launch-demo-windows-vm01" to Windows VM
	- Review properties of Linux VM - no public IP, security group, etc.
	- Review properties of Windows VM - public IP, security group, etc.
7. Create new Elastic IP and assign to Linux VM
	- Allocate from Amazon pool
	- Associate address to Linux VM
	- Review properties of Linux VM again
8. Use WSL to connect to Linux VM over SSH
	- Copy .pem file from /mnt/c/Downloads... to ~ in WSL session
	- Run `chmod 0400 ~/launch-demo-linux-key.pem` - changes permissions to make readable by our WSL session
	- Execute `ssh -i ~/launch-demo-linux-key.pem ubuntu@<ip address or DNS name>` (other default user names depending on 	OS)
	- Execute `cd /etc` and `ls` to show interaction
	- Demonstrate `sudo apt-get update`
	- Demonstrate `sudo apt-get upgrade`
	- After, in Management Console, stop instance
9. Connect to Windows VM using Remote Desktop
	- Use "Connect" button in Management Console
	- Use .pem key file to pull secure password
	- Click "Download Remote Desktop File" and run
	- Enter secure password (copy/paste)
10. Build RDS (SQL Server)
	- Use name "launch-demo-mssql-dbserver01"
	- Use Standard configuration
	- Use 2017 + free tier
	- Attach to VPC with new subnet group (associated to 2 public subnets)
	- Additional connectivity --> public access
11. While RDS is being built out
	- Login to Windows VM
	- Unblock Internet for admins (Server Manager)
	- Install Chrome
	- Install Git (https://git-scm.com/)
	- Install .NET Core SDK (https://dotnet.microsoft.com/download/dotnet-core)
	- Clone code repository for demo app
    - Review PaaS demo diagram
12. After RDS instance creation completes
	- Click on DB and security group --> add my IP for access (1433)
	- Connect via SSMS
    - Create new database ("launch-demo-db")
	- Run T-SQL script to create new table and populate with demo data
13. On Windows VM, demo web API locally
	- **Update connection string to point to new RDS instance - usually not stored in source code (security risk); instead will be saved in a secrets management solution like Hashicorp Vault, Azure Key Vault or AWS Key Management Service (KMS)**
	- From web app project location (in an Administrator command prompt), run `C:\Program Files\dotnet\dotnet.exe build`
	- From web app project location (in an Administrator command prompt), run `C:\Program Files\dotnet\dotnet.exe run`
	- Try accessing list of projects, see that fails
	- Grant Windows VM Security Group access to port 1433 in RDS Security Group
	- Try accessing list of projects again - on success, retrieve specific, create one, edit one and delete one
14. Demonstrate ability to access Web App remotely from local machine
	- Add port 30000 to Windows VM Security Group (use `0.0.0.0/0`) but talk about security risk (Hub & Spoke)
	- Add Windows Firewall Rule for port 30000 (demo web app)
	- Test Web App through VM remotely - list projects and list specific project

## Lambda - Demo

Under AWS services, search for "Lambda" and highlight AWS "tagline"

1. Create new Lambda function using "Use a blueprint"
2. Under "Blueprints", search for "hello-world"
3. Choose the nodejs starter and click "Configure"
4. Use "launch-demo-lambda" for function name
5. Select "Create new role with basic Lambda permissions":
6. Leave function code as-is and click "Create function"
7. Replace lambda function code with "index.js" code in repo; talk about how you can modify code right in this window
8. Also talk about how you can add additional .js files, etc. in the "Function code" area to build out a more complex program
9. Do a quick walkthrough of code
10. Highlight some of the other available configuration options with the lambda function
11. Click "Save"; talk about ability to test right from here
12. Click "Add trigger" and choose API Gateway
13. Create an API using the REST API option
14. Under security, set API key
15. Click "Add"
16. Quickly highlight options for "Add destination"; click "Cancel"
17. Highlight the API configuration and function code views in the lambda integration
18. Click the "API Gateway" name to navigate to the service; also talk about accessing using "API Gateway" service in Management Console
19. Drill into available method
20. Review diagram outlining flow and steps through the proxy to the lambda function
21. Talk about purpose of API Keys --> subscription
22. Click "Resources", select root ("/") and under "Actions", select "Deploy API"; deploy to "default" stage and note "Invoke URL" - copy/paste to Notepad
23. Click "API Keys", click "launch-demo-lambda-Key", click "Show" and copy/paste key to Notepad
24. In POSTMan, choose POST, use "Invoke URL" + "/launch-demo-lambda" and use `{ "key1": "Value 1", "key2": "Value 2", "key3": "Value 3" }` for request body
25. Attempt to send and see the "Forbidden" response
26. Add the "x-api-key" header with API Key copied previously
27. Attempt POST again - see success
28. Talk about ability to send combo of query strings and body
29. Quickly highlight throttling settings, and others

## Containers - Demo

1. In WSL, create new folder called "launch-demo-containers"
2. Change to "launch-demo-containers" folder and run `dotnet new mvc`
3. Run `dotnet build`
4. Run `dotnet run` and navigate to http://localhost:5000 in a browser window to test
5. From directory, execute `code .` and create a new Dockerfile in the root of the app folder and copy contents from repo
6. Execute `docker build -t demo-image .` to build a new Docker image for the app
7. Execute `docker run -it --rm -p 8000:5000 -e ASPNETCORE_URLS=http://+:5000 --name demo-container demo-image`
8. Navigate to http://localhost:8000 in a browser window to test (running in container)
9. Create a new docker-compose.yml file in the root of the app folder and copy contents from repo
10. Execute `docker-compose build` and then `docker-compose up`
11. Navigate to http://localhost:8020 in a browser window to test (running in container)
12. Review images and containers in VS Code
13. From WSL, run `docker exec -it launch-demo-containers_db_1 /bin/bash`
14. Navigate to `/opt/mssql-tools/bin` on interactive container shell
15. Execute `./sqlcmd -S . -d master -U sa -P Passw0rd123!`
16. Run `CREATE TABLE Person (FirstName nvarchar(50), LastName nvarchar(50));GO`
17. Run `INSERT INTO Person (FirstName, LastName) VALUES ('Melissa', 'Testing');`
18. Run `INSERT INTO Person (FirstName, LastName) VALUES ('Bob', 'Roberts');GO`
19. Run `SELECT * FROM Person;GO`
20. Ctrl+C to leave sqlcmd and run `exit` to leave container shell
21. In VS Code, delete containers
22. In VS Code, delete images
23. Exit VS Code and delete /launch-demo-containers folder