# World of Warcraft Mechanics Visualizer

A full-stack .NET application developed to map and visualize the complex relationships between factions, races, and classes in World of Warcraft. This project serves as a demonstration of a modern client-server architecture, utilizing a standalone frontend and a centralized RESTful API.

## 🏛 Architecture & Tech Stack

The application is built with a focus on a **layered architecture**, ensuring that data management, business logic, and the user interface remain independent and maintainable.

* **Frontend:** **Blazor WebAssembly (WASM)** – A high-performance Single Page Application (SPA) providing a seamless user experience using C# and .NET.
* **Backend:** **ASP.NET Core Web API** – A RESTful service that manages business rules, handles requests, and interfaces with the data layer.
* **Database:** **MySQL** – A relational database used to store and link complex game entities and quiz modules.
* **ORM:** **Entity Framework Core** – Used for efficient data mapping and management of relational schemas.

## 🌟 Key Technical Features

* **Relational Mapping of Game Data:** Implements server-side logic to filter and serve valid race/class combinations based on World of Warcraft's game mechanics.
* **Dynamic Image & Data Fetching:** Frontend components utilize asynchronous API calls to retrieve high-quality images and descriptions based on unique entity IDs.
* **Interactive Knowledge Engine:** A dynamic quiz module integrated into the sidebar that fetches questions and validates answers directly through the API.
* **State Management & Routing:** Leverages Blazor’s client-side routing and component-based state management for a responsive, desktop-like feel.

## ⚖️ Handling Dual-Faction Logic
One of the technical challenges was managing "Neutral" or "Dual-faction" races (e.g., Pandaren or Dracthyr). 
* **Backend Logic:** The API handles these through a specific FactionID or a many-to-many bridge table that allows a single Race ID to be associated with multiple Faction IDs.
* **Frontend Visualization:** The UI dynamically renders faction-specific themes and icons based on the data returned, ensuring the user understands which races have the unique ability to choose their side.

## 📊 Database Design
The project utilizes a relational MySQL schema designed to handle:
* **Entity Relationships:** Links between races and their specific playable classes.
* **Categorization:** Grouping of races into factions and classes into roles.
* **Asset Management:** Mapping database IDs to file paths for dynamic image rendering in the UI.

## ⚙️ Installation & Setup

1.  **Clone the Repository:**
    ```bash
    git clone [https://github.com/YourUsername/YourRepoName.git](https://github.com/YourUsername/YourRepoName.git)
    ```
2.  **Database Configuration:**
    * Update the connection string in the Web API's `appsettings.json`.
    * Apply migrations: `dotnet ef database update`.
3.  **Run the Backend:**
    * Navigate to the API project and run `dotnet run`.
4.  **Run the Frontend:**
    * Navigate to the Blazor project and run `dotnet run`.

## 🎓 About the Project
This project was developed as a graduation thesis to showcase proficiency in the .NET ecosystem, focusing on building scalable, maintainable, and user-centric web applications.
