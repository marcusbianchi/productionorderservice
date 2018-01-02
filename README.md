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

# ProductionOrderAPI

API to Manage Production Order on Lorien. Used to create, update, read and delete Production Order.

## Production Order Data Format

These are the fields of the Production Order and it's constrains:

* productionOrderId: Id of the Production Order given by de Database.
  * Integer
  * Ignored on Create, mandatory on the other methods
* productionOrderNumber: Number of The Production Order
  * String (Up to 50 chars)
  * Mandatory
  * Unique
* quantity: Quantity of The PO
  * Integer
  * Mandatory
* recipe: Recipe of the Production Number ("recipeId" is the only mandatory field in the Recipe Json Body)
  * Recipe JSON
  * Mandatory
* productionOrderTypeId: Id of Production Order Type
  * Integer
  * Mandatory
* typeDescription: Descritpion of the PO Type
  * String (Up to 50 chars)
  * Ignored

### JSON Example:

```json
{
  "productionOrderId": 1,
  "recipe": {
    "recipeId": 36,
    "recipeName": "teste43",
    "recipeCode": "2228887",
    "phases": [
      {
        "phaseId": 8,
        "phaseName": "testefundido",
        "phaseCode": "323232",
        "phaseProducts": [],
        "phaseParameters": []
      }
    ]
  },
  "productionOrderNumber": "121231321321",
  "productionOrderTypeId": 2,
  "typeDescription": "Tira",
  "quantity": 0
}
```

## URLs

* api/productionorders/{optional=startat}{optional=quantity}

  * Get: Return List of Production Order
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
  * Post: Create the Production Order with the JSON in the body
    * Body: Production Order JSON

* api/productionorders/{id}

  * Get: Return Production Order with productionOrderId = ID

# GatewayAPI

API Responsible to provide access to information nedeed to compose the production order from other APIs

## URLs

* gateway/recipes/

  * Get: Return List of recipes

* gateway/recipes/{id}

  * Get: Return Recipe with recipeId = ID

# StateConfigurationAPI

API to Manage The possible states for Order Types on Lorien. Used to update and read the state configuration of a production Order Type.

## StateConfiguration Data Format

These are the fields of the StateConfiguration and it's constrains:

* productionOrderTypeId: Id of the Production Order Type of which this configuration belongs to.
  * Integer
  * Ignored on Create, mandatory on the other methods
* states: possible states of this production order type.
  * Array State objects
* state: Value of the current state
  * Enum
  * Possible Values: created, active, inactive, paused, ended, waiting_approval, approved, reproved
* possibleNextStates: possible values after the current one
  * List Enum
  * Possible Values: created, active, inactive, paused, ended, waiting_approval, approved, reproved

### JSON Example:

```json
{
  "productionOrderTypeId": 2,
  "states": [
    {
      "state": "created",
      "possibleNextStates": ["inactive", "waiting_approval"]
    },
    {
      "state": "waiting_approval",
      "possibleNextStates": ["approved", "reproved"]
    }
  ]
}
```

## URLs

* api/stateconfiguration/{productionOrderTypeId}

  * Get: Return state configuration of the Production Order Type where productionOrderTypeId=productionOrderTypeId
  * Put: Update the configuration of the Production Order Type where productionOrderTypeId=productionOrderTypeId
    * Body: StateConfiguration JSON

# StateManagementAPI

API to Manage the current state of a production order on Lorien. Used to update the status of the production order.

## URLs

* /api/productionorders/statemanagement/number{productionordernumber}{state}

  * Put: Update the configuration of the Production Order Type where productionordernumber=productionordernumber to the Status = State (Valid States: created, active, inactive, paused, ended, waiting_approval, approved, reproved)

* /api/productionorders/statemanagement/id{productionorderid}{state}

  * Put: Update the configuration of the Production Order Type where productionorderid=productionorderid to the Status = State (Valid States: created, active, inactive, paused, ended, waiting_approval, approved, reproved)

## After Status Change Post

This API can send the Data to a Endpoint if the configuration in present. The API will send a production order JSON to the configured endpoint.
