# WoWActivities

![Deploy status](https://github.com/ghostdog87/WoWActivities/actions/workflows/docker-push.yml/badge.svg)

https://wowactivities.fly.dev/

Project used to experiment with ASP.NET Core, React JS and practicing with DevOps for developers and pipelines.

Current versions of frameworks are .NET 7.0 and React 18.

Tools, libraries and techniques used in this project so far:

- Using React 18 with Typescript
- Using SignalR to enable real time web communication to a chat feature in activity details view
- React MobX library for state management 
- Axios for fetching/posting data from client app
- Clean Architecture 
- Automatically deploying the project on Fly.io as Docker container using GitHub Workflow Actions and Pipelines
- Using PostgreSQL as Database and Entity Framework Core for database management (code first approach)
- MediatR library (it applies CQRS / Mediator pattern)
- Setting up and configured ASP.NET Core identity for authentication
- Client side login and register to React application
- Client side routing with React Router 6.0
- Mapping models and DTOs using AutoMapper library
- Building the UI using Semantic UI components
- Photo Upload widget for user profile with photo gallery
- Using Formik and Yup for forms and their validation
- Paging, Sorting and Filtering features for activity list
- Getting an 'A' rating for security - TODO
- Authentication with Facebook and Github - TODO
