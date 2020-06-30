# Web-Programming-If-Match
Assignment 10 of Bethel University Fall 2019's Web Programming course

This assignment contains one WebApi project with a controller titled GargoyleController
and an HTML page with javascript to make requests to the controller. The javascript
keeps track of a current ETag with GET requests so it can use PATCH with If-Match.
Overall, this ensures that the gargoyle objects do not overwrite one another.

- GargoyleModel
  - Class represents a gargoyle
  - All gargoyles have 4 string properties: Name, Color, Size, and Gender. They
  must each have a length of at least 3, but only Name is required. Gargoyles also
  have an "Updated" property that is of type DateTime which has a value of when the
  gargoyle was last updated.
- GargoyleDatabase
  - Maintains a list of gargoyles currently in the system. The Name of the gargoyle
  is the "key" of the database, so it uses dictionaries to store the gargoyles.
- GargoyleController
  - Supports 5 endpoints:
    - GET
    - GET/{index} (a specific gargoyle): sets an ETag header representing the gargoyle
    in some way. GET uses the gargoyle's Name as the URL parameter, not an integer
    index.
    - POST: does not allow 2 gargoyles with the same name to be in the system at the
    same time (error shows if an attempt is made to create a new gargoyle of a
    preexisting name.
    - PUT/{index}: replaces a gargoyle already at that index or creates it if it
    doesn't exist. PUT validates that the gargoyle name that is being added/replaced
    matches the URL index parameter. It also verifies that the If-Match header of the
    request either is a wild card "*", or matches the ETag value of the gargoyle
    being replaced.
    - PATCH requests verify that the If-Match header of the request is either a wild
    card or matches the ETag value of the gargoyle to be edited.
