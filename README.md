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
  "typeDescription": "Tira",
  "typeScope": "OP-Tira",
  "stateConfiguration": {
    "productionOrderTypeId": 1,
    "states": [
      {
        "state": "created",
        "possibleNextStates": ["inactive", "active"]
      },
      {
        "state": "active",
        "possibleNextStates": ["paused", "ended"]
      },
      {
        "state": "paused",
        "possibleNextStates": ["active", "ended"]
      },
      {
        "state": "ended",
        "possibleNextStates": []
      },
      {
        "state": "inactive",
        "possibleNextStates": []
      }
    ]
  },
  "thingGroups": [
    {
      "thingGroupId": 1,
      "groupName": "teste",
      "groupCode": "teste"
    },
    {
      "thingGroupId": 2,
      "groupName": "Equipamentos",
      "groupCode": "Historian"
    }
  ],
  "thingGroupIds": [1, 2]
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
* currentstatus: Current Status of the production order
  * String
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
  "quantity": 0,
  "currentstatus:"active"
}
```

## URLs

* api/productionorders/{optional=startat}{optional=quantity}{optional=orderField}{optional=order}{optional=fieldFilter}{optional=fieldValue}
  * Get: Return List of Production Order
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
    * orderField: Field in which the list will be order by (Possible Values:
      productionOrderNumber,currentstatus)
      productGTIN)(Default=ProductId)
    * order: Represent the order of the listing (Possible Values: ascending,
      descending)(Default=Ascending)
    * fieldFilter: represents the field that will be seached (Possible Values:
      productionOrderNumber,currentstatus) (Default=null)
    * fieldValue: represents de valued searched on the field (Default=null)
  * Post: Create the Production Order with the JSON in the body
    * Body: Production Order JSON

* api/ProductionOrders/v2?{optional=startat}{optional=quantity}{optional=orderField}{optional=order}{optional=filters}
  * Get: Return List of Production Order
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
    * orderField: Field in which the list will be order by (Possible Values:
      productionOrderNumber,currentstatus)
      productGTIN)(Default=ProductId)
    * order: Represent the order of the listing (Possible Values: ascending,
      descending)(Default=Ascending)
    * filters: List represents the field that will be seached (Possible Values:
      productionOrderNumber,currentstatus) (Default=null) and (virgule) and represents de valued searched on the field (Default=null)
      Ex: api/ProductionOrders/v2?filters=productionOrderTypeId,1&filters=currentStatus,reproved

* api/productionorders/{id}
  * Get: Return Production Order with productionOrderId = ID

* api/productionorders/thing/{thingid}
  * Get: Return Production Order with currentthing = thingid

# GatewayAPI

API Responsible to provide access to information nedeed to compose the production order from other APIs

## URLs

* gateway/recipes/{optional=startat}{optional=quantity}{optional=orderField}{optional=order}{optional=fieldFilter}{optional=fieldValue}

  * Get: Return List of Recipes

    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
    * orderField: Field in which the list will be order by (Possible Values:
      recipeName,recipeCode)(Default=RecipeId)
    * order: Represent the order of the listing (Possible Values: ascending,
      descending)(Default=Ascending)
    * fieldFilter: represents the field that will be seached (Possible Values:
      recipeName,recipeCode)(Default=null)
    * fieldValue: represents de valued searched on the field (Default=null)

* gateway/recipes/{id}

  * Get: Return Recipe with recipeId = ID

* gateway/thinggroups/{optional=startat}{optional=quantity}

  * Get: Return List of Groups of Things
    * startat: represent where the list starts at the database (Default=0)
    * quantity: number of resuls in the query (Default=50)

* gateway/thinggroups/{id}

  * Get: Return Group of Things with thingGroupId = ID

* gateway/thinggroups/attachedthings/{groupid}

  * Get: List of Thing inside the group where thingGroupId = ID

* gateway/things/{id}

  * Get: Thing where thingId = ID

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

# AssociateProductionOrderAPI

API used to associate a Production Order with a thing. When a Thing is associated with a production order his status is set in the correct api.

## URLs

* api/productionorders/AssociateProductionOrder/associate/{thingId}{productionOrderId}
  * Put: Associate the Production Order where productionOrderId=productionOrderId to the thing where thingid=thingid IF THIS IS PERMITED.
* api/productionorders/AssociateProductionOrder/disassociate/
  * Put: Disassociate the Production Order where productionOrderId=productionOrderId to the thing where thingid=thingid IF THIS IS PERMITED.
    * Body: productionOrder JSON

## After Association Post

This API can send the Data to a Endpoint if the configuration in present. The API will send a production order JSON to the configured endpoint.

## Historian State Production Order
Endpoint with historian changes states of the production orders

* histStatesId: Id of the historian state by de Database.
  * Integer
* productionOrderId: Id of The Production Order
  * Integer
* state: state of production order
  * String
* date: datetime of the historian in ticks
  * Long

### JSON Example:

```json
[
    {
        "histStatesId": 1,
        "productionOrderId": 1,
        "state": "created",
        "date": 636582793631549624
    },
    {
        "histStatesId": 2,
        "productionOrderId": 1,
        "state": "active",
        "date": 636582793901537505
    }
]
```
## URLs
* api/ProductionOrdersHistStates?{ProductionOrderId=productionOrderId}

  * Get: Return list with historian production order
    * productionOrderId: id of production order

* api/ProductionOrdersHistStates/productionOrderList?{StatusSearch=status}&{StartDate=startDate}&{EndDate=endDate}

  * Get: Return list int with productionOrderId that are in the date range
    * StatusSearch: status in search
    * StartDate: start date range in ticks
    * EndDate: end date range in ticks

* api/ProductionOrdersHistStates?{ProductionOrderId=productionOrderId}&{State=state}

  * Post: Create Historian of state in production order
    * productionOrderId: id of production order
    * state: state for historian 
