let mutations = {


    FETCH_USER(state, user) {

        return state.user = user
    },

    FETCH_EVENTS1(state, events) {

        return state.events1 = events;
    },

    FETCH_EVENTS2(state, events) {

        return state.events2 = events;
    },

    FETCH_PO(state, pos) {

        return state.pos = pos;
    },

    FETCH_WORKSITEID(state, id) {

        return state.worksiteID = id;
    },

    FETCH_PRODUCTIONLINEID(state, id) {

        return state.productionlineID = id;
    },

    FETCH_DOWNTIME_REASONS(state, downtimeReasons){

        return state.downtimeReasons = downtimeReasons;
    },

    FETCH_DOWNTIME_REASONS_2(state, downtimeReasons_2){

        return state.downtimeReasons_2 = downtimeReasons_2;
    },

    FETCH_SPEEDLOSSES(state, speedLoss){

        return state.speedLoss = speedLoss;
    },


    FETCH_DOWNTIME_REASONS_MACHINE_ISSUE(state, machineIssue){
        return state.machineIssue = machineIssue;
    },

    CREATE_PO(state, POs){
        state.PO.unshift(POs);
    },

    FETCH_SITES(state, sites){
        return state.sites = sites;
    },

    FETCH_MACHINES(state, machines){
        return state.machines = machines;
    },

    FETCH_PERFORMANCE_INDEXES(state, performanceIndexes){
        return state.performanceIndexes = performanceIndexes;
    },

    FETCH_ALL_EVENTS(state, events){
        return state.allEvents = events;
    },

    FETCH_QUALITY_LOSSES(state, qualityLosses){
        return state.qualityLosses = qualityLosses;
    },

    FETCH_ALL_VOLUMES(state, volumes){
        return state.volumes = volumes;
    },

    FECTH_ASSIGNATION(state, assignation){
        return state.assignation.push(assignation);
    },

    FECTH_CHECKPO(state, checkPO){
        return state.checkPO = checkPO;
    },

    FETCH_NETOP(state, data){
        return state.netOP = data ;
    },

    FETCH_UNPLANNED_DOWNTIME_EVENTS(state, events) {
        return state.unplannedDowntimeEvents = events;
    },

    FETCH_UNPLANNED_DOWNTIME_MACHINE_SHUTDOWN_TYPES(state, events) {
        return state.unplannedDowntimeMachineShutdownTypes = events;
    },

    CREATE_UNPLANNEDEVENT_UNPLANNEDDOWNTIME(state, unplannedEvent){
        state.unplannedEvent_UnplannedDowntime.unshift(unplannedEvent);
    },


    CREATE_UNPLANNEDEVENT_CHANGINGFORMAT(state, unplannedEvent) {
        state.unplannedEvent_ChangingFormat.unshift(unplannedEvent)
    },

    CREATE_UNPLANNEDEVENT_CLIENTCHANGING(state, unplannedEvent) {
        state.unplannedEvent_ClientChanging.unshift(unplannedEvent)
    },

    CREATE_ASSIGNATION(state, assignation) {
        state.assignation.unshift(assignation)
    },

    CREATE_UNPLANNEDEVENT_CIP(state, unplannedEvent) {
        state.unplannedEvent_CIP.unshift(unplannedEvent)
    },

    CREATE_PLANNEDEVENT(state, plannedEvent) {
        state.plannedEvent.unshift(plannedEvent)
    },

    CREATE_SPEEDLOSS(state, speedLoss) {
        state.speedLoss.unshift(speedLoss)
    },

    STOP_PO(state, PO) {
        state.PO.unshift(PO)
    },

    STORE_REJECTION(state, REJECTION) {
        state.REJECTION.unshift(REJECTION)
    },

    RETREIVETOKEN(state, token) {
        state.token = token;
    },

    GET_SPEED_LOSSES(state, sl) {
        state.getSpeedLosses = sl;
    }






};
export default mutations
