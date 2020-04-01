# EduPartner

A prototype education centre web application for parents to manage the enrollments of their children.

## Demo Site

The demo website can be accessed at https://edupartner-prototype.herokuapp.com. (**NOTE**: Access might be a bit slow as the site is hosted on a European server.)

### Demo Features

No authentication is required to access these features (the log in button on the home page is a mock and leads straight to the dashboard). The demo starts off with you logged in as a parent with two children. One of the children is enrolled for one class.

#### Dashboard

![](https://imgur.com/Tdnc5St)

Quickly access an overview of your children's upcoming classes for the next 7 days, or use one of the quick links to access other key features of the site.

#### Children

![](https://imgur.com/JedHFOW)

See an overview of the children that you have registered with the site. You can also register a new child from this page. Alternatively, you can view each child's details by clicking "View" on the right of each child.

![](https://imgur.com/bpwAvEM)

For each child, you can see the child's details as well as the subjects that you have enrolled them for. You can enroll them for additional subjects from this page.

#### Schedule

![](https://imgur.com/vY3sxN2)

See a monthly calendar view of your children's classes in one spot.

#### Invoices

![](https://imgur.com/egAbyI4)

![](https://imgur.com/HIJcVot)

View invoices that are due for payment based on your children's enrollments and make payments for them directly within the site.

## Installation and Usage

### Prerequisites

- .NET Core 3.1

### Installation

`git clone https://github.com/pyrox18/EduPartner.git`

### Usage

Either:

- `dotnet run src/EduPartner.MvcApp/EduPartner.MvcApp.csproj`, or
- Open the solution file in Visual Studio and run it from there

Currently, the application does not need a database; it uses the in-memory provider for EF Core. However, this means that data will not be persisted across application restarts.

To manually reset the database to its original seed data while the application is running, click the "Admin Tools" link in the footer, then "Reset Database".
