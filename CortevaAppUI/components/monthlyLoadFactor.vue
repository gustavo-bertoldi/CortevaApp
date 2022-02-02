<template>

<div>
    <div class="d-flex">

        <!--{{allEvents['CP']}}-->


        <!--{{allEvents}}-->


        <div class="col">


            <div class="d-flex">
                <form>
                    <label class="" for="site">Site : </label>
                    <select name="site" id="site" class="form-select" v-model="site">
                        <template v-for="site in sites[0]">
                            <option v-bind:value="site.name">
                                {{site.name}}
                            </option>
                        </template>
                    </select>
                </form>


            </div>


            <div class="d-flex">
                <form>
                    <label class="" for="productionline">Ligne de production : </label>
                    <select name="productionline" id="productionline" class="form-select" v-model="productionline">
                        <template v-for="productionline in sites[1]">
                            <template v-if="productionline.name === site">
                                <option v-bind:value="productionline.productionline_name">
                                    {{productionline.productionline_name}}
                                </option>
                            </template>

                        </template>

                    </select>
                </form>
            </div>


            <div class="d-flex">
                <form>
                    <label class="" for="PO">Numéro de PO : </label>
                    <input type="text" id="PO" class="" v-model="PONumber">
                </form>
            </div>


            <br/>
            <input v-on:click="load()" type="button" class="btn btn-outline-info" value="Charger">


        </div>

        <div class="col">
            <h3>
                Monthly Load Factor –
                One year overview


            </h3>
            <br/>
            <div class="d-flex">
                <label class="" for="startingPO">De</label>
                <input type="date" id="startingPO" class=" " required v-model="beginningDate">


                <label class="" for="endingPO">A</label>
                <input type="date" id="endingPO" class=""
                       required v-model="endingDate">
            </div>

            <br/>


            <br/>



            <br/>

        </div>




    </div>

    <br/>
    <br/>
    <template v-if="show===1">
        <div class="table-info-data">
            <table class="table">
                <thead>
                <tr>
                    <th scope="col"></th>
                    <th scope="col">Jan</th>
                    <th scope="col">Feb</th>
                    <th scope="col">Mar</th>
                    <th scope="col">Apr</th>
                    <th scope="col">May</th>
                    <th scope="col">Jun</th>
                    <th scope="col">Jul</th>
                    <th scope="col">Aug</th>
                    <th scope="col">Sep</th>
                    <th scope="col">Oct</th>
                    <th scope="col">Nov</th>
                    <th scope="col">Dec</th>


                </tr>
                </thead>
                <tbody>
                <tr>
                    <th scope="row">Plant Operating Time</th>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>

                </tr>

                <tr>
                    <th scope="row">Line Operating Time <br/> Planned Downtime</th>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>

                </tr>

                <tr>
                    <th scope="row">Nb Production</th>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>

                </tr>

                <tr>
                    <th scope="row">Planned Maintenance <br/> Capital Project <br/> Breaks, Meal</th>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>

                </tr>

                <tr>
                    <th scope="row">Load Factor</th>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>
                    <td>0</td>

                </tr>
                </tbody>
            </table>
        </div>
    </template>

</div>






</template>

<script>
    import {mapGetters} from "vuex";

    export default {
        name: "monthlyLoadFactor",

        data() {
            return {
                site: '',
                productionline: '',
                product: '',
                formulationType: '',
                reporting: '',
                tool: '',
                beginningDate: '',
                endingDate: '',
                username: sessionStorage.getItem("username"),
                index: -1,
                show: 0,
                PONumber: '',
            }
        },

        methods: {
            load: async function () {
                var index = 0;
                for (let i = 0; i < this.sites[1].length; i++) {

                    if (this.sites[1][i].productionline_name === this.productionline) {
                        index = this.sites[1][i].id;
                    }
                }




                if (this.productionline !== '' && this.beginningDate !== '' && this.endingDate !== '') {
                    var tab = [];
                    tab.push(this.site);
                    tab.push(this.productionline);
                    tab.push(this.beginningDate);
                    tab.push(this.endingDate);
                    tab.push(this.PONumber);

                    this.$store.dispatch('fecthAllEvents', tab);
                    await this.resolveAfter15Second();
                    this.show = 1;
                    this.index = index;


                }

                this.show = 1;

            },

            resolveAfter15Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 1500);
                });
            },


            resolveAfter05Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 500);
                });
            },


        },

        mounted() {
            if(sessionStorage.getItem("language") !== null){
                this.$i18n.locale = sessionStorage.getItem("language");
            }
            this.$store.dispatch('fetchSites');

        },

        computed: {
            ...
                mapGetters([
                    'sites',
                    'allEvents',
                ])
        }
    }
</script>

<style scoped>
    h1 {
        font-size: 1.4em;
        color: #56baed;
    }

    h2 {
        font-size: 1.2em;
        color: #56baed;
    }

    h3{
        color: #56baed;

    }

    div {
        background-color: #fff;
        padding: 15px;
    }

    thead {
        color: white;
        background: #56baed;
    }


    .rcorners2 {
        border: 2px solid lightgray;
        padding: 20px;
    }
</style>
