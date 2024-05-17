<template>
  <layout>
    <page-header title="Schedule Date" :sub_title="'Schedule date between '+member_one_name +' - ' + member_two_name"></page-header>

    <div class="w-full flex justify-left items-center overflow-hidden mt-4">
      <div class="flex gap-6 items-center mt-1 w-1/2 border-b border-gray-200 py-2">
        <input type="text" v-model="date_member_name" class="w-full px-4 py-2 rounded border border-gray-200 text-sm font-normal text-gray-700 focus:outline-none">
      </div>
      <div class="p-2 flex gap-2 items-center">
        <button @click="showPreview=!showPreview" class="flex px-4 py-2 text-sm font-semibold text-white rounded bg-blue-600">Preview</button>
        <button @click="back" class="flex px-4 py-2 text-sm font-semibold text-white rounded bg-red-500">Back</button>
      </div>
    </div>
    <div class="mt-6">
      <div class="flex gap-4 items-center">
        <Datepicker v-model="interview_start_date" :format="dateFormat" :enable-time-picker="false" autoApply placeholder="Date" class="px-2 py-1 text-sm font-normal "></Datepicker>

        <div class="flex gap-2 items-center">
          <input v-model="interview_start_time" type="time" class="w-[90px] rounded p-2 border border-gray-200 text-sm font-norma">
          <p class="text-sm font-normal text-gray-600 px-2">To</p>
          <input type="time" v-model="interview_end_time" class="w-[90px] rounded p-2 border border-gray-200 text-sm font-norma">
        </div>

        <div class=" flex gap-2 items-center">
          <Multiselect
              class="w-[350px] text-xs h-11" style="padding-left: 0px !important;min-width: 300px; max-width: 500px !important; font-size: 13px"
              v-model="time_zone"
              :multiple="false"
              placeholder="Select TimeZone"
              :searchable="true"
              :options="timeZoneList"
          />

          <p class="w-full text-sm font-medium text-gray-600">Time Zone</p>
        </div>
      </div>

      <div class="flex gap-6 mt-6">
        <div class="w-1/2">

          <div class="w-full my-2.5">
            <label class="text-sm text-gray-700 font-medium mb-2 ml-2">Select Schedule Type</label>
            <div class="flex items-center gap-6 p-2">
              <div class="flex items-center gap-2.5">
                <input v-model="schedule_status" value="in_person" name="schedule_status"  type="radio" class="w-4 h-4 rounded-full border border-gray-500">
                <div class="text-small font-medium text-gray-800">In Person</div>
              </div>
              <div class="flex items-center gap-2.5">
                <input v-model="schedule_status" value="online" name="schedule_status" type="radio" class="w-4 h-4 rounded-full border border-gray-500">
                <div class="text-small font-medium text-gray-800">Online</div>
              </div>
            </div>

          </div>

          <div  v-if="schedule_status.toLocaleLowerCase() === 'online' " class=" w-full p-2 bg-gray-200 rounded mt-2">

            <button v-if="!meeting_url" @click="handleAuthClickForOnline" class="flex px-4 py-2 text-sm font-semibold text-white rounded bg-blue-600">Add Google Meet Video Conferencing</button>

            <div v-if="meeting_url" class="mt-2 flex gap-2 items-center">
              <button  @click="openMeeting" class="flex px-4 py-2 text-sm font-semibold text-white rounded bg-blue-600">Join with Google Meet</button>
              <span @click="closeMeeting" class="material-symbols-outlined text-base text-gray-600 cursor-pointer">close</span>
            </div>
            <p class="text-sm px-2 py-2 font-normal text-gray-700">{{meeting_url}}</p>
          </div>

          <div v-if="schedule_status.toLocaleLowerCase() === 'in_person' " class="flex gap-4 items-center">
            <label class="flex items-center text-sm text-gray-700 font-medium ml-2">Place</label>
            <div class="w-full flex items-center gap-2 p-2">
              <GoogleAddressAutocomplete
                  :apiKey="google_api_key"
                  v-model="google_location"
                  @callback="callbackFunction"
                  class="w-full flex items-center  gap-2 h-9 rounded p-4"
                  placeholder="Add location"
                  @blur="addInPersonPlace"
              />
            </div>

          </div>

          <div class="flex gap-4 items-center mt-4">
            <div>
              <div v-for="(ele, index) in notifications" :key="index" class="flex gap-2 items-center px-2 py-2 rounded">

                <p class="py-1 text-sm font-normal ">Notification</p>

                <input v-model="ele.time" type="number" min="0" max="24" class="ml-2 w-[80px] bg-gray-100 border-0 h-6 rounded p-4" placeholder="">

                <select v-model="ele.type" class="w-[180px] border-0 flex gap-2 bg-gray-100 items-center h-8 px-2 py-1 rounded">
                  <option value="minutes" >Minutes</option>
                  <option value="hours" >Hours</option>
                  <option value="days" >Days</option>
                </select>

                <button @click="deleteRow(index)" type="button" class="focus:outline-none text-gray-600 ml-4">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </button>

              </div>
            </div>
          </div>
          <div class=" mt-2">
            <p @click="addRow" class="text-base font-medium cursor-pointer text-gray-600 ml-2">Add notification</p>
          </div>

          <div class="w-full bg-gray-200 rounded mt-8 mb-3">
            <tinymce :description="mail_body" fieldEmitName="tinymce_change"></tinymce>
          </div>
        </div>
        <div class="w-1/2">
          <div class="w-1/2">
            <div class="flex gap-4 items-center border-b-2 border-gray-200">
              <a href="#" class="text-base font-semibold text-blue-600 py-2 px-1 border-b-2 border-blue-600">Members</a>
            </div>
            <div class="mt-5">
              <div class="flex gap-4 items-center">
                <img class="w-10 h-10 rounded-full" :src="page_data.member_one && page_data.member_one.user_face_media && page_data.member_one.user_face_media.url? page_data.member_one.user_face_media.url : '/project-assets/images/user.jpg' ">
                <div>
                  <p class="text-sm font-normal text-gray-800">{{page_data.member_one ? page_data.member_one.first_name + ' ' + page_data.member_one.last_name:""}}</p>
                  <p class="text-sm font-normal text-gray-800">{{page_data.member_one ? page_data.member_one.email:""}}</p>
                </div>
              </div>
              <div class="flex gap-4 items-center mt-4">
                <img class="w-10 h-10 rounded-full" :src="page_data.member_two && page_data.member_two.user_face_media && page_data.member_two.user_face_media.url? page_data.member_two.user_face_media.url: '/project-assets/images/user.jpg' ">
                <div>
                  <p class="text-sm font-normal text-gray-800">{{page_data.member_two ? page_data.member_two.first_name + ' ' + page_data.member_two.last_name:""}}</p>
                  <p class="text-sm font-normal text-gray-800">{{page_data.member_two ? page_data.member_two.email:""}}</p>
                </div>
              </div>

            </div>

          </div>
        </div>
      </div>
    </div>

    <div v-if="showPreview " class="fixed inset-0 bg-opacity-25 w-full bg-gray-800 z-10 ">
      <div  class="flex items-center w-full h-full">
        <div class="md:w-[520px] bg-white border w-full rounded-md m-auto">

          <div class="w-full flex items-center justify-between border-b h-12 px-4">
            <div class="text-base font-semibold text-gray-700">Mail Preview</div>

            <div class="flex items-center justify-end">

              <button v-if="authorized && schedule_status === 'online'"  @click.prevent="createSchedule" class="flex px-4 mx-2 py-2 text-sm font-semibold text-white rounded bg-blue-600">Schedule Now </button>
              <button v-if="authorized && schedule_status !== 'online'"  @click.prevent="handleAuthClickForInPerson" class="flex px-4 mx-2 py-2 text-sm font-semibold text-white rounded bg-blue-600">Schedule Now </button>

              <div @click.prevent="showPreview= false" class="w-5 h-5 rounded flex items-center bg-gray-100 text-gray-600 hover:bg-red-500 hover:text-white cursor-pointer">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2.5" stroke="currentColor" class="w-4 h-4 m-auto">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
                </svg>
              </div>
            </div>
          </div>
          <div class="w-full p-4 rounded-md flex items-center justify-center overflow-y-auto" style="max-height: 540px; ">
            <div class="mt-1.5 px-2 py-2"  v-html="mail_content"> </div>
          </div>
        </div>
      </div>
    </div>

  </layout>
</template>