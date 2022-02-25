<template v-if="eventsPerProductionline.length > 0">
  <div>
    <div class="container">

      <!--<div class="d-flex flex-row justify-content-between align-items-center bg-white">-->


      <div align="center" class="rcorners2 table-info-data">
        <h4>{{$t("downtimesHistory")}}</h4>
        <br>

        <table class="table">
          <thead>
          <tr>
            <th scope="col">{{$t("type")}}</th>
            <th scope="col">{{$t("entryTime")}}</th>
            <th scope="col">{{$t("duration(Minutes)")}}</th>
            <th scope="col">{{$t("comments")}}</th>

          </tr>
          </thead>
          <tbody>
            <tr v-for="event in eventsPerProductionline[productionName]" :key="event">
              <th scope="row">{{$t(event.type)}}</th>
              <td>{{event.updated_at.split('T')[1]}}</td>
              <td>{{event.total_duration}}</td>
              <td>{{event.comment}}</td>
            </tr>
          </tbody>
        </table>

      </div>
      <!--</div>-->

      <br>

      <div align="center" class="productionName rcorners2">
        {{$t(title)}}
      </div>

    </div>


    <br/>

    <form id="form">


      <div class="form-group row">
        <label class="col-sm-2 col-form-label rcorners1" for="totalDuration">{{$t("duration(Minutes)")}}</label>
        <div class="col-sm-10">
          <input type="number" id="totalDuration" class="form-control-plaintext rcorners2" required>
        </div>
      </div>

      <div class="form-group row">
        <label for="comments" class="col-sm-2 rcorners1">{{$t("comments")}}</label>
        <div class="col-sm-10">
                    <textarea id="comments">
                    </textarea>

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

        <button class="btn btn-primary d-flex align-items-center btn-warning" id="addReasonButton" type="button"
                @click.prevent="addReason()">
          {{$t("addAReason")}}
        </button>


        <button class="btn btn-primary border-success align-items-center btn-success" type="button"
                @click.prevent="validateInformations()">
          OK
        </button>
      </div>
    </form>


  </div>
</template>

<script>
import axios from "axios";
import router from "@/router";
import { urlAPI} from "@/variables";

export default {
  name: "PlannedEvent_Pannel2",

  data() {
    return {
      url: sessionStorage.getItem("url"),
      productionName: sessionStorage.getItem("productionName"),
      productionlines: sessionStorage.getItem("prodlines").split(','),

      downtimeType: sessionStorage.getItem("downtimeType"),
      indice: sessionStorage.getItem("indice"),
      PO: sessionStorage.getItem("pos").split(','),

      parameters: [],
      printedStep: 0,
      title: sessionStorage.getItem("reasonDowntime"),

      Planned_Event: {


        OLE: '',
        productionline: '',
        reason: '',
        duration: 10,
        comment: '',
      },


      eventsPerProductionline: [],
      eventsUser: null,


    }
  },

  methods: {

    validateInformations: function () {
      this.Planned_Event.OLE = sessionStorage.getItem("poNumber");
      this.Planned_Event.productionline = sessionStorage.getItem("productionName");
      this.Planned_Event.reason = sessionStorage.getItem("reasonDowntime");

      this.Planned_Event.duration = document.getElementById('totalDuration').value;
      this.Planned_Event.comment = document.getElementById('comments').value;

      console.log(sessionStorage.getItem("pos"));


      console.log(this.Planned_Event);

      if ( this.Planned_Event.duration  > 0) {
        axios.post(urlAPI + "plannedEvent", this.Planned_Event);

        console.log(this.Planned_Event);

        this.backOrigin();

      } else {
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

    addReason : function(){
      this.Planned_Event.OLE = sessionStorage.getItem("pos").split(',')[this.indice];
      this.Planned_Event.productionline = sessionStorage.getItem("productionName");
      this.Planned_Event.reason = sessionStorage.getItem("reasonDowntime");

      this.Planned_Event.duration = document.getElementById('totalDuration').value;
      this.Planned_Event.comment = document.getElementById('comments').value;


      axios.post(urlAPI + "plannedEvent", this.Planned_Event);


      console.log(this.Planned_Event);
      router.replace("/eventDeclaration/"+this.productionName+"/plannedDowntime");
      window.location.reload();

    },

    backOrigin: function () {

      router.replace("/homePage");


    },


    resolveAfter05Second: function () {
      return new Promise(resolve => {
        setTimeout(() => {
          resolve('resolved');
        }, 500);
      });
    },

    backPage: function () {

      router.replace("/eventDeclaration/"+this.productionName+'/'+this.downtimeType);

    },
    resolveAfter15Second: function () {
      return new Promise(resolve => {
        setTimeout(() => {
          resolve('resolved');
        }, 1000);
      });


    },


  },

  async mounted() {

    if (sessionStorage.getItem("language") !== null) {
      this.$i18n.locale = sessionStorage.getItem("language");
    }

    for (let i = 0; i < this.productionlines.length; i++) {

      this.eventsPerProductionline.push(this.productionlines[i]);

      this.eventsPerProductionline[this.eventsPerProductionline[i]] = [];

      await axios.get(urlAPI + 'events/' + this.PO[i] + '/' + this.productionlines[i])
          .then(response => (this.eventsUser = response.data))

      await this.resolveAfter15Second();


      for (let j = 0; j < this.eventsUser.length; j++) {
        for (let k = 0; k < this.eventsUser[j].length; k++) {
          this.eventsPerProductionline[this.productionlines[i]].push(this.eventsUser[j][k]);
        }
      }
    }
    console.log(this.eventsPerProductionline);


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

#addReasonButton {
  color: black;

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

.table-info-data {
  overflow:scroll; max-height: 300px;
}


</style>
