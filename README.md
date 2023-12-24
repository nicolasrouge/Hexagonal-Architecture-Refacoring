# Hexagonal Architecture Refactoring

➡️ Hello there! I'm undertaking an exciting exercise: refactoring the project originally developed by Pauline and Caroline from Agicap, in partnership with Artisan Developpeur <https://youtu.be/G8ItpRMBcH0>. The project showcases an approach to Domain-Driven Design (DDD) with a particular focus on *hexagonal architecture*.

Really good exercise. Thanks to Pauline Jamin, Caroline Desplanques & Artisan Developpeur.

# My Notes about the refactoring 📝
## Architecture Overview: 3-Tier to Hexagonal Transition 
### Original Structure: 3-Tier
- **Direct Interaction:** `BookingService` directly calls the repository. 
- **Tightly Coupled:** Business logic closely tied to data access. 
### Refactored: Ports and Adapters (Hexagonal)
- **Ports (Interfaces):** `IProvideBar` in the domain for abstraction. 
- **Adapters:** 'Bar Adapter and Repos' implement these interfaces, connecting to the domain. 
- **Inverted Dependencies:** External components reference the domain, not the other way around. 
- **Benefits:** Decoupled design, enhanced flexibility, and improved testability.

This transition shifts our architecture towards better modularity, aligning with Domain-Driven Design principles.

# Description (see the forked project)

The idea of this project is : we are many devs all around Europe and when devs are coming to Lyon, we like to celebrate and go to a bar. 
So we did a fictionnal little API to find the best date and bar to gather developers. The project was done a long time ago in n-tier architecture and did not evolve since.

On `main` you will find the base code.

## Product requirements
### Rule #1: boats
Summer is coming and we would like to add a new data source for boat bars in order to chill next to the Rhone river. If possible, boats are always our first choice and must be booked instead of an indoor bar.
Try to implement this without refactoring. Then do as much refactoring as needed.

### Rule #2: bar capacity
Bars are complaining that we fill up to much space. we would like to book only bars where we fill less than 80% of their capacity.


### Rule #3: rooftops
We are happy we added boats, but we forgot about rooftops ! it is another data source.


## Conclusion
 On `proposition-one` you will find one proposition where we went towards a more DDD-like approach. 
 
 It is far from perfect, but it allows us to implement rules 2 and 3 much more easily.
