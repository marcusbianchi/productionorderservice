# ProductionOrderTypeAPI

API to Manage Production Order Types on Lorien. Used to create, update, read and delete Production Order Types.

## Production Order Types Data Format

These are the fields of the Production Order Type and it's constrains:

* productionOrderTypeId: Id of the Production Order Type given by de Database.
  * Integer
  * Ignored on Create, mandatory on the other methods
* typeDescription: Description of the Type.
  * String (Up to 50 chars)
  * Mandatory
* typeScope: Scope Value when used in States
  * String (Up to 50 chars)
  * Mandatory

### JSON Example:

```json
{
  "productionOrderTypeId": 1,
  "typeDescription": "Liga",
  "typeScope": "OP-Liga"
}
```

## URLs

* api/productionordertypes/{optional=startat}{optional=quantity}

  * Get: Return List of Production Order Types
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
  * Post: Create the Production Order Type with the JSON in the body
    * Body: Production Order Type JSON

* api/productionordertypes/{id}

  * Get: Return Production Order Type with productionOrderTypeId = ID
  * Put: Update the Production Order Type with the JSON in the body with productionOrderTypeId = ID
    * Body: Production Order Type JSON
  * Delete: Delete the Production Order Type from the Database with productionOrderTypeId = ID
