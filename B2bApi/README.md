<a name="readme-top"></a>

## B2B API

B2bApi is a modular monolith created in ASP.NET WebAPI. The project is related to the 2-day coding challenge. 
The purpose of the project was to assess the performance of combining Ef Core + Dapper in a single monolith.
It is just a demo and does not include all functionalities. This project leverages modern
tools and technologies to ensure performance, scalability, and ease of maintenance.
Architectural decisions can be found in the [Architecture Decision Record](Architecture-decisions).

### Prerequisites

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

### Installation

#### Step 1 - Clone the repository
```bash
git clone https://github.com/twoje-repo/B2bApi.git
cd B2bApi
```

#### Step 2 - Run docker-compose
For the development environment (by default), a Docker Compose configuration has been prepared. 
To set up the environment (database and logging), use the following command:

```bash
docker-compose up
```
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Project description

**1. Registration + Login with JWT Tokens**

The B2B portal enables users to register and log in to the system. The authentication process is based 
on JWT (JSON Web Token), providing a secure means of communication between the client and the server. 
Registered users receive unique tokens that are subsequently used for authorizing access to protected resources.

_Check [code](Shared/Security) for details_

### 2. Product Management

**2.1 Product List for All Users**

The portal provides a feature to browse the list of products available in the offer. Each user can view the available products.
Response is paginated and sorted by id (default). Price is converted to the currency of the user's choice. 

**2.2 Adding Products**

Administrators have the authority to add new products to the database. This process is restricted to the administrator 
role to ensure controlled management of the product assortment.

### 3. Loyalty Points Calculation

After making a purchase, the system calculates loyalty points for registered users. This scoring can later be used 
within the loyalty program to obtain various benefits.

### 4. Retrieving Current Currency Exchange Rates

The portal periodically updates currency exchange rates by fetching them from an external source, such as the NBP API. 
The current rates are utilized within the system to convert product prices into various currencies.

_Check [code](ExchangeRates/Jobs) for details_

### 5. Adding Product Discounts

Administrators have the authority to add discounts to products for a specified period. This allows for managing promotions

### 6. Product Availability Management

The product availability feature enables administrators to manage product availability in the warehouse.

### 7. Cart Management

Users, after logging in, can add products to the shopping cart. The cart allows users to summarize the selected products,
calculate costs, and move through the stages of the ordering process.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## API documentation

After running the application, the API documentation is available at [ReDoc](https://localhost:5058/api-docs/index.html).

## Testing

The project contains unit tests and integration tests. To run the tests, use the following command:

```bash
dotnet test
```

## License

Distributed under the MIT License.

<p align="right">(<a href="#readme-top">back to top</a>)</p>