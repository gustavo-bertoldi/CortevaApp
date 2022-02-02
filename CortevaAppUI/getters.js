let getters = {

    user: state => {

        return state.user
    },

    pos: state => {

        return state.pos
    },

    worksiteID: state => {

        return state.worksiteID
    },

    productionlineID : state =>{
        return state.productionlineID
    },

    sites : state =>{
      return state.sites;
    },

    allEvents : state =>{
        return state.allEvents;
    },

    qualityLosses : state =>{
        return state.qualityLosses;
    },

    unplannedDowntimeEvents: state => {
        return state.unplannedDowntimeEvents;
    },


    unplannedDowntimeMachineShutdownTypes: state => {
        return state.unplannedDowntimeMachineShutdownTypes;
    },

    machines : state => {
        return state.machines;
    },


    events1: state => {

        return state.events1
    },

    events2: state => {

        return state.events2
    },


    speedLoss: state => {

        return state.speedLoss
    },

    getSpeedLosses: state => {
        return state.getSpeedLosses
    },

    volumes: state => {

        return state.volumes;
    },

    checkPO: state => {

        return state.checkPO;
    },

    assignation: state => {
        return state.assignation
    },

    downtimeReasons: state => {

        return state.downtimeReasons
    },

    netOP: state => {

        return state.netOP
    },

    performanceIndexes: state => {
        return state.performanceIndexes;
    },

    downtimeReasons_2: state => {

        return state.downtimeReasons_2
    },


    machineIssue: state => {

        return state.machineIssue
    },
    /**
    posts: state => {
        return state.posts
    },

    questions: state => {
        return state.questions
    },

    courses: state => {
        return state.courses
    },
     */
};

export default  getters
