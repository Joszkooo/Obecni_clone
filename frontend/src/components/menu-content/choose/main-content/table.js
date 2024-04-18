import {decodeCredential, GoogleLogin, googleLogout} from "vue3-google-login";
import axios from "axios";
const API_URL="http://localhost:5182/";
export default {
    components: {GoogleLogin},
    data(){
        return{
            loggedIn: false,
            user: null,
            notes: [],
            status1: [],

            callback:(response)=> {
                console.log("zalogowano")
                this.loggedIn = true
                console.log(response)
                this.user = decodeCredential(response.credential)

            }
        }
    },
    methods:{
        async showTable() {
            axios.get(API_URL + "api/todoapp/GetNotes").then(
                (response)=>{
                    this.notes=response.data;
                }
            )
        },
        showlaststatus(id) {
            axios.get(API_URL + "api/todoapp/ShowStatus?id="+id).then(
                (response)=>{
                    //console.log("Status:", response.data[0].Status);
                    return response.data[0].Status;
                }
            )
        },
        changeStatus(id){
            axios.post(API_URL + "api/todoapp/ChangeStatus?id="+id).then(
                (response)=>{
                    alert(response.data);
                }
            )
        },
        logout(){
            googleLogout()
            this.loggedIn = false
        },

        verify(email){
            axios.get(API_URL + "api/todoapp/Verify?email="+email).then(
                (response)=>{
                    alert(response.data);
                }

            )
        }
    },mounted:function() {
        this.showTable()
    }
}