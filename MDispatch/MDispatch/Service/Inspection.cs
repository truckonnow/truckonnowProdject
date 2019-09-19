﻿using MDispatch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MDispatch.Service
{
    public class Inspection
    {
        public int ReCurentStatus(string token, string id, ref string description, string status)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/ReCurentStatus", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idShip", id);
                request.AddParameter("status", status);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SendBolEmaile(string token, string idship, ref string description, string email)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Email/BOL", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idShip", idship);
                request.AddParameter("email", email);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SendCouponEmaile(string token, ref string description, string email)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Email/Copon", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("email", email);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveSigPikedUp(string token, Photo photoSig, string id, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string sigPhoto = JsonConvert.SerializeObject(photoSig);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/SaveSigPikedUp", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVech", id);
                request.AddParameter("jsonSigPhoto", sigPhoto);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int GetShipping(string token, string id, ref string description, ref Shipping shipping)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Shipping", Method.POST);
                client.Timeout = 600000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idShip", id);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description, ref shipping);
            }
        }

        public int SaveAsk(string token, string id, Ask ask, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(ask);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 1);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception e)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveAsk(string token, string id, Ask2 ask, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(ask);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 6);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception e)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveAsk(string token, Feedback feedback, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(feedback);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/FeedBack", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveAsk(string token, string id, Ask1 ask1, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                //EncodeJPEG(ask1.Any_additional_documentation_been_given_after_loading);
                //EncodeJPEG(ask1.Any_additional_parts_been_given_to_you);
                //EncodeJPEG(ask1.App_will_force_driver_to_take_pictures_of_each_strap);
                //EncodeJPEG(ask1.Photo_after_loading_in_the_truck);
                string strJsonAsk = JsonConvert.SerializeObject(ask1);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                client.Timeout = 60000;
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 2);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveAsk(string token, string id, AskFromUser askForUser, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(askForUser);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 3);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveAsk(string token, string id, AskDelyvery askDelyvery, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(askDelyvery);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                client.Timeout = 60000;
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 4);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveAsk(string token, string id, AskForUserDelyveryM askForUserDelyveryM, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(askForUserDelyveryM);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                client.Timeout = 60000;
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 5);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        [Obsolete]
        public int SavePhoto(string token, string id, PhotoInspection photoInspection, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                if (photoInspection.Damages != null)
                {
                    photoInspection.Damages.ForEach((dm) =>
                    {
                        dm.Image = null;
                        dm.ImageSource = null;
                    });
                }
                //EncodeJPEG(photoInspection.Photos);
                string strPhotoInspection = JsonConvert.SerializeObject(photoInspection);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Photo", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStr", strPhotoInspection);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception e)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        protected byte[] OptimizeNResize(Bitmap original_image, double width, double heidth)
        {
            MemoryStream memoryStream = new MemoryStream();
            Bitmap final_image = null;
            Graphics graphic = null;
            int reqW = (int)width;
            int reqH = (int)heidth;
            final_image = new Bitmap(reqW, reqH);
            graphic = Graphics.FromImage(final_image);
            graphic.FillRectangle(new SolidBrush(System.Drawing.Color.Transparent),
                new System.Drawing.Rectangle(0, 0, reqW, reqH));
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic; 
            graphic.DrawImage(original_image, 0, 0, reqW, reqH);
            final_image.Save(memoryStream, ImageFormat.Jpeg);
            if (graphic != null) graphic.Dispose();
            if (original_image != null) original_image.Dispose();
            if (final_image != null) final_image.Dispose();
            return memoryStream.ToArray();
        }

        public int SaveDamageForuser(string token, string idVech, List<DamageForUser> damageForUsers, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                if (damageForUsers != null)
                {
                    damageForUsers.ForEach((dm) =>
                    {
                        dm.Image = null;
                        dm.ImageSource = null;
                    });
                }
                string strDamageForUsers = JsonConvert.SerializeObject(damageForUsers);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Damages/User", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVech", idVech);
                request.AddParameter("damageForUserJson", strDamageForUsers);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SavePhotPay(string token, string idVech, int type, Photo photo, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string photojson = JsonConvert.SerializeObject(photo);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Pay", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVech", idVech);
                request.AddParameter("type", type);
                request.AddParameter("Photo", photojson);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveVideoRecount(string token, string idVech, int type, Video video, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string videojson = JsonConvert.SerializeObject(video);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/Recount", Method.POST);
                client.Timeout = 600000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVech", idVech);
                request.AddParameter("type", type);
                request.AddParameter("video", videojson);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SaveMethodPay(string token, string idVech, string payMethod, string countPay, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Save/PickedUp/PayMethod", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idVech", idVech);
                request.AddParameter("payMethod", payMethod);
                request.AddParameter("countPay", countPay);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        private int GetData(string respJsonStr, ref string description)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                return 3;
            }
            else
            {
                description = responseAppS
                    .Value<string>("description");
                return 2;
            }
        }

        private int GetData(string respJsonStr, ref string description, ref Shipping shipping)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                shipping = JsonConvert.DeserializeObject<Shipping>(responseAppS.
                        SelectToken("ResponseStr").ToString());
                return 3;
            }
            else
            {
                description = responseAppS
                    .Value<string>("description");
                return 2;
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void EncodeJPEG(List<Photo> photos)
        {
            if (photos != null)
            {
                photos.ForEach((dm) =>
                {
                    Bitmap bmp1 = new Bitmap(new MemoryStream(Convert.FromBase64String(dm.Base64)));
                    ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                    System.Drawing.Imaging.Encoder myEncoder =
                        System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 0L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    MemoryStream memoryStream = new MemoryStream();
                    bmp1.Save(memoryStream, jgpEncoder, myEncoderParameters);
                    dm.Base64 = Convert.ToBase64String(memoryStream.ToArray());
                });
            }
        }
    }
}