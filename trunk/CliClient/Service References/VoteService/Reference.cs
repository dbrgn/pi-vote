﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pirate.PiVote.CliClient.VoteService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VoteService.VoteServiceSoap")]
    public interface VoteServiceSoap {
        
        // CODEGEN: Generating message contract since element name request from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Execute", ReplyAction="*")]
        Pirate.PiVote.CliClient.VoteService.ExecuteResponse Execute(Pirate.PiVote.CliClient.VoteService.ExecuteRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ExecuteRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Execute", Namespace="http://tempuri.org/", Order=0)]
        public Pirate.PiVote.CliClient.VoteService.ExecuteRequestBody Body;
        
        public ExecuteRequest() {
        }
        
        public ExecuteRequest(Pirate.PiVote.CliClient.VoteService.ExecuteRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ExecuteRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] request;
        
        public ExecuteRequestBody() {
        }
        
        public ExecuteRequestBody(byte[] request) {
            this.request = request;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ExecuteResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ExecuteResponse", Namespace="http://tempuri.org/", Order=0)]
        public Pirate.PiVote.CliClient.VoteService.ExecuteResponseBody Body;
        
        public ExecuteResponse() {
        }
        
        public ExecuteResponse(Pirate.PiVote.CliClient.VoteService.ExecuteResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ExecuteResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] ExecuteResult;
        
        public ExecuteResponseBody() {
        }
        
        public ExecuteResponseBody(byte[] ExecuteResult) {
            this.ExecuteResult = ExecuteResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface VoteServiceSoapChannel : Pirate.PiVote.CliClient.VoteService.VoteServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VoteServiceSoapClient : System.ServiceModel.ClientBase<Pirate.PiVote.CliClient.VoteService.VoteServiceSoap>, Pirate.PiVote.CliClient.VoteService.VoteServiceSoap {
        
        public VoteServiceSoapClient() {
        }
        
        public VoteServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VoteServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VoteServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VoteServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Pirate.PiVote.CliClient.VoteService.ExecuteResponse Pirate.PiVote.CliClient.VoteService.VoteServiceSoap.Execute(Pirate.PiVote.CliClient.VoteService.ExecuteRequest request) {
            return base.Channel.Execute(request);
        }
        
        public byte[] Execute(byte[] request) {
            Pirate.PiVote.CliClient.VoteService.ExecuteRequest inValue = new Pirate.PiVote.CliClient.VoteService.ExecuteRequest();
            inValue.Body = new Pirate.PiVote.CliClient.VoteService.ExecuteRequestBody();
            inValue.Body.request = request;
            Pirate.PiVote.CliClient.VoteService.ExecuteResponse retVal = ((Pirate.PiVote.CliClient.VoteService.VoteServiceSoap)(this)).Execute(inValue);
            return retVal.Body.ExecuteResult;
        }
    }
}
