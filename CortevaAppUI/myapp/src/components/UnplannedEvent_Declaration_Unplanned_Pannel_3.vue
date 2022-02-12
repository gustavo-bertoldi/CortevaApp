<template>

  <div>

    <div align="center" class="productionName rcorners2">

      {{$t(title)}}

    </div>

    <br/>

    <template v-if="printedStep < 2">

      <template v-if="printedStep === 0">
        <div class="row" align="center">
          <div class="col-sm-4" v-for="issue in machineIssue" :key="issue.id">
            <button
                class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
                type="button" @click.prevent="chooseMachineImplicated(issue.component, issue.other_machine)">
              {{$t(issue.component)}}
            </button>

          </div>
        </div>

      </template>

      <template v-if="printedStep === 1">
        <div class="row" align="center">

          <div class="col-sm-4" v-for="machine in downtimeReasons_2" :key="machine.id">
            <button
                class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
                type="button" @click.prevent="addComments(machine.name)">
              {{machine.name}}
            </button>

          </div>

        </div>

      </template>

      <div align="left">
        <button type="button" class="btn btn-danger" @click.prevent="backPage()">
          {{$t("back")}}
        </button>
      </div>

    </template>


    <template v-if="printedStep === 2">
      <form id="form">
        <div class="form-group row">
          <label class="col-sm-2 col-form-label rcorners1" for="time">{{$t("duration(Minutes)")}}</label>
          <div class="col-sm-10">
            <input type="number" id="time" class="form-control-plaintext rcorners2">
          </div>
        </div>

        <div class="form-group row">
          <label for="comments" class="col-sm-2 rcorners1">{{$t("comments")}}</label>
          <div class="col-sm-10">
            <textarea type="text" id="comments" class="form-control-plaintext rcorners2"></textarea>
          </div>
        </div>


        <div align="right">
          <button class="btn btn-primary border-danger align-items-right btn-danger" type="button"
                  @click.prevent="backOrigin()">
            {{$t("cancel")}}
          </button>
        </div>
        <div class="d-flex flex-row justify-content-between align-items-center bg-white">
          <button class="btn btn-primary d-flex align-items-center btn-danger" type="button"
                  @click.prevent="backPage()">
            {{$t("back")}}
          </button>

          <button class="btn btn-primary border-success align-items-center btn-success" type="button"
                  @click.prevent="validateInformations()">
            OK
          </button>
        </div>
      </form>

    </template>

  </div>




</template>

<script>
import router from "@/router";
import {default as axios} from "axios";
const urlAPI = 'http://localhost:13197/api/';


export default {
  name: "UnplannedEvent_Declaration_Unplanned_Pannel_3",data() {
    return {
      url: sessionStorage.getItem("url"),
      productionName: sessionStorage.getItem("productionName"),
      downtimeType: sessionStorage.getItem("downtimeType"),
      indice: sessionStorage.getItem("indice"),

      parameters: [],
      printedStep: 0,
      title: '',
      previousTitle: '',
      otherMachine :0,

      componentName: '',

      machineImplicated :'',

      downtimeReasons_2: null,
      machineIssue:null,

      unplannedEvent: {

        /**
         * planned ou unplanned
         * le type d'arret
         * la raison de l'arret
         * la machine conecernee
         * le composant concerne
         * la duree
         * le commentaire
         */

        OLE: '',

        productionline: sessionStorage.getItem("productionName"),

        //planned ou unplanned
        downtimeType: sessionStorage.getItem("downtimeType"),

        //la ligne de production
        productionLine:  sessionStorage.getItem("productionName"),

        //nom de la machine
        implicated_machine: '',

        //le composant concerné
        component : '',

        //duree
        total_duration : 0,

        //commentaire
        comment :'',

        type : '',
      },


    }
  },

  methods: {

    chooseMachineImplicated : function(componentName, otherMachine){

      this.componentName = componentName;
      this.otherMachine = otherMachine;

      if(otherMachine  === 1){
        this.previousTitle = this.title;
        this.title = 'Machine impliquée';
        this.printedStep = 1;
      }else{
        this.previousTitle = this.title;
        this.title = this.$t(componentName);
        this.printedStep = 2;
        this.machineImplicated = componentName;
      }



    },

    addComments: function (machineImplicated) {

      this.machineImplicated = machineImplicated;
      this.printedStep = 2;

      this.previousTitle = this.title;

      this.title = machineImplicated;


    },

    validateInformations : function(){
      this.unplannedEvent.OLE = sessionStorage.getItem("poNumber");
      this.unplannedEvent.implicated_machine = this.machineImplicated;
      this.unplannedEvent.component = this.componentName;
      this.unplannedEvent.total_duration = document.getElementById('time').value;
      this.unplannedEvent.comment = document.getElementById('comments').value;

      console.log(this.unplannedEvent);



      if(this.unplannedEvent.total_duration  > 0){
        axios.post(urlAPI + "unplannedEvent/unplannedDowntime", this.unplannedEvent);
        this.backOrigin();
      }else{
        this.errorMessage();
      }

    },

    errorMessage : function(){
      var h1 = document.getElementsByClassName("error");
      if(h1.length <= 0){
        let error = document.createElement('h1');
        error.setAttribute("class", "error");
        error.innerHTML = this.$t("errorInput");
        error.setAttribute("style", "color:red;")
        error.setAttribute("align", "center");
        let br = document.createElement('br');
        let form = document.getElementById("form");
        form.insertBefore(br, form.firstChild);
        form.insertBefore(error, form.firstChild);
      }

    },
    backOrigin : function(){

      router.replace('/homePage');


    },




    resolveAfter05Second: function () {
      return new Promise(resolve => {
        setTimeout(() => {
          resolve('resolved');
        }, 500);
      });
    },

    backPage: function () {

      if(this.printedStep >0){
        if(this.otherMachine === 1){
          this.printedStep --;
          this.title = this.previousTitle;
        }else{
          this.printedStep = 0;
          this.title = this.previousTitle;
        }

      }else{
        router.replace('/eventDeclaration/'+this.productionName+'/unplannedDowntime');
      }

    }


  },

  async mounted() {

    if (sessionStorage.getItem("language") !== null) {
      this.$i18n.locale = sessionStorage.getItem("language");
    }

    this.parameters.push(this.productionName);
    this.parameters.push(this.downtimeType);

    await axios.get(urlAPI + 'getMachines/' + this.productionName + '/unplannedDowntime/unplannedDowntime')
        .then(response => (this.downtimeReasons_2 = response.data))


    if (this.downtimeType === "unplannedDowntime") {
      this.title = this.$t("filler");
    } else {
      this.title = this.$t("unplannedDowntime");
    }

    await axios.get(urlAPI + 'unplannedDowntime/unplannedDowntime/filler')
        .then(response => (this.machineIssue = response.data))


    //while(this.downtimeReasons_2.length === 0){
    //  this.resolveAfter05Second();
    //}

  },

}

</script>


<style scoped>


.productionName {
  left: 0;
  top: 0;
  min-width: 150px;
  max-width: 250px;

  margin-bottom: 50px;
}

.rcorners1 {
  border-radius: 25px;
  background: lightblue;
  padding: 20px;
  margin-bottom: 30px;
  width: 180px;

}


.rcorners2 {
  border-radius: 25px;
  border: 2px solid lightblue;
  padding: 20px;
}

.wrapper {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
}


button {
  color: white;
  margin-top: 20px;


}

input {
  width: 40%;

}


#comments {
  height: 150px;
  width: 70%;
  border-radius: 25px;
  border: 2px solid lightblue;
  padding: 20px;

}

h1 {
  color: red;
}

</style>
